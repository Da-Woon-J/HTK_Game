import vector

class VectorStabilizer:
    def __init__(self, threshold=3.0, smoothing=0.6):
        # :param threshold: 이 수치 이하의 평균 움직임은 노이즈로 간주하고 무시합니다.
        # :param smoothing: 0.0 ~ 1.0. 1.0에 가까울수록 원본 좌표를 그대로 따르고, 낮을수록 부드러워집니다.
        
        self.threshold = threshold
        self.smoothing = smoothing
        self.prev_lm = None

    def update(self, current_lm):
        # 첫 감지이거나 랜드마크 개수가 안 맞으면 현재 값으로 초기화
        if not self.prev_lm or len(current_lm) != len(self.prev_lm):
            self.prev_lm = current_lm
            return current_lm

        total_distance = 0.0

        # 21개 랜드마크의 이동 거리 계산
        for curr, prev in zip(current_lm, self.prev_lm):
            total_distance += vector.Euclidean_Dis(curr, prev)

        # 랜드마크 평균 이동 거리
        avg_distance = total_distance / len(current_lm)

        # Deadzone
        if avg_distance < self.threshold:
            return self.prev_lm

        # 임계값 이상 움직였으면 보간하여 부드럽게 업데이트
        stabilized_lm = []
        for curr, prev in zip(current_lm, self.prev_lm):
            new_x = prev[0] + (curr[0] - prev[0]) * self.smoothing
            new_y = prev[1] + (curr[1] - prev[1]) * self.smoothing
            new_z = prev[2] + (curr[2] - prev[2]) * self.smoothing
            stabilized_lm.append([new_x, new_y, new_z])

        self.prev_lm = stabilized_lm
        return stabilized_lm