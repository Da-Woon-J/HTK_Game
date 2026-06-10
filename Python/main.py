import cv2
from cvzone.HandTrackingModule import HandDetector
import udp_socket
import gesture
import normalization
from stabilizer import VectorStabilizer  # 1. 스테빌라이저 클래스 임포트


cap = cv2.VideoCapture(0)

detector = HandDetector(maxHands=2, detectionCon=0.8)

stabilizers = {
    'Left': VectorStabilizer(threshold=3.0, smoothing=0.6),
    'Right': VectorStabilizer(threshold=3.0, smoothing=0.6)
}

GAIN = 1.8 

while True:
    success, img = cap.read()

    # 캠을 읽어오지 못했을 때
    if not success:
        print("캠 신호 x, 연결을 확인하세요.")
        break

    # 캠 화면의 중심점 계산
    h, w, _ = img.shape
    cx, cy = w // 2, h // 2

    # Hands 감지
    hands, img = detector.findHands(img)

    data = []

    if hands:
        for hand in hands:
            hand_type = hand['type']  #'Left', 'Right'
            lm_normalized = normalization.Normalize(hand['lmList'])
            lm_stabilized = stabilizers[hand_type].update(lm_normalized)
            hand_gesture = gesture.gestures(lm_stabilized)
            lm_gained = []
            for lm in lm_stabilized:
                gx = cx + (lm[0] - cx) * GAIN
                gy = cy + (lm[1] - cy) * GAIN
                gz = lm[2] * GAIN
                lm_gained.append([gx, gy, gz])
            data.extend([lm_gained, hand_type, hand_gesture])

    print(data)
    udp_socket.send_udp(data)

    # 핸드 트래킹 디버그
    cv2.imshow("Hand Tracking", img)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()