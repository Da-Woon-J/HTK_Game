import vector

def Normalize(handsLM):
    wrist = handsLM[0]

    #wist ~ middle_mcp / 손목 ~ 중지 뿌리 거리 계산
    scale = vector.Euclidean_Dis(handsLM[0], handsLM[9])
    if scale == 0: scale = 1 # 스케일 0 방지

    fixed_size = 150.0 #손 크기 세팅

    normalized = []
    for lm in handsLM:
        # 크기 왜곡 제거 / 손목을 뺀 순수 방향(비율)에 고정 크기를 곱함
        offset_x = ((lm[0] - wrist[0]) / scale) * fixed_size
        offset_y = ((lm[1] - wrist[1]) / scale) * fixed_size
        offset_z = ((lm[2] - wrist[2]) / scale) * fixed_size

        # 웹캠의 원래 손목 위치(절대 좌표)에 크기가 고정된 뼈대를 이어 붙임
        normalized.append([
            wrist[0] + offset_x,
            wrist[1] + offset_y,
            wrist[2] + offset_z
        ])
    
    return normalized