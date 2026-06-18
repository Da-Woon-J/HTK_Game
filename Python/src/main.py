import cv2
from cvzone.HandTrackingModule import HandDetector
import udp_socket
import gesture
import normalization
from stabilizer import VectorStabilizer

cap = cv2.VideoCapture(0)


detector = HandDetector(maxHands=2, detectionCon=0.8)

stabilizers = {
    'Left': VectorStabilizer(threshold=3.0, smoothing=0.6),
    'Right': VectorStabilizer(threshold=3.0, smoothing=0.6)
}

GAIN = 5.0 

while True:
    success, img = cap.read()

    if not success:
        print("캠 신호 x, 연결을 확인하세요.")
        break

    h, w, _ = img.shape
    cx, cy = w // 2, h // 2

    hands, img = detector.findHands(img)

    data = []

    if hands:
        for hand in hands:
            hand_type = hand['type']
            lm_normalized = normalization.Normalize(hand['lmList'])
            lm_stabilized = stabilizers[hand_type].update(lm_normalized)
            hand_gesture = gesture.gestures(lm_stabilized)   

            wrist = lm_stabilized[0]

            gained_wrist_x = cx + (wrist[0] - cx) * GAIN
            gained_wrist_y = cy + (wrist[1] - cy) * GAIN
            gained_wrist_z = wrist[2] * GAIN
            lm_gained = []
            for lm in lm_stabilized:
                offset_x = lm[0] - wrist[0]
                offset_y = lm[1] - wrist[1]
                offset_z = lm[2] - wrist[2]
                
                lm_gained.append([
                    gained_wrist_x + offset_x,
                    gained_wrist_y + offset_y,
                    gained_wrist_z + offset_z
                ])
            
            data.extend([lm_gained, hand_type, hand_gesture])

    print(data)
    udp_socket.send_udp(data)

    # 핸드 트래킹 디버그
    cv2.imshow("Hand Tracking", img)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()