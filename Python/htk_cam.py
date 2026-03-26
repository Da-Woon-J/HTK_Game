import cv2
from cvzone.HandTrackingModule import HandDetector
import udp_socket

cap = cv2.VideoCapture(0)

# htk 초기화
detector = HandDetector(maxHands=2, detectionCon=0.8)

while True:
    success, img = cap.read()

    # 캠을 읽어오지 못했을 때
    if not success:
        print("캠 신호 x, 연결을 확인하세요.")
        break

    # Hands 감지
    hands, img = detector.findHands(img)

    data = []


    if hands:
        hand_0 = hands[0]
        lm_0 = hand_0['lmList']
        type_0 = hand_0['type']
        data.extend([lm_0,type_0])
        if len(hands) == 2:
            hand_1 = hands[1]
            lm_1 = hand_1['lmList']
            type_1 = hand_1['type']
            data.extend([lm_1,type_1])
        print(data)
        udp_socket.send_udp(data)

    # 핸드 트래킹 디버그
    cv2.imshow("Hand Tracking", img)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()