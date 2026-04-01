import math

def Euclidean_Dis(coordinate_0,coordinate_1):
    return math.sqrt(
        (coordinate_0[0] - coordinate_1[0])**2 +
        (coordinate_0[1] - coordinate_1[1])**2 +
        (coordinate_0[2] - coordinate_1[2])**2
    )