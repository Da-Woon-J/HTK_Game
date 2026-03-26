import math

def Euclidean_Dis(coordinate_0,coordinate_1):
    return math.sqrt(
        (coordinate_0[0] - coordinate_1[0])**2 +
        (coordinate_0[1] - coordinate_1[1])**2 +
        (coordinate_0[2] - coordinate_1[2])**2
    )

def gestures(handsLM):
    wrist = handsLM[0]
    thumb_tip = handsLM[4]
    index_tip = handsLM[8]
    middle_tip = handsLM[12]
    ring_tip = handsLM[16]
    pinky_tip = handsLM[20]

    grabList= [
        Euclidean_Dis(thumb_tip, wrist),
        Euclidean_Dis(index_tip, wrist),
        Euclidean_Dis(middle_tip, wrist),
        Euclidean_Dis(ring_tip, wrist),
        Euclidean_Dis(pinky_tip, wrist)
    ]
    grab_Threshold = 150
    grab_Checksum = 0
    for grab in grabList:
         if(grab<grab_Threshold):
              grab_Checksum += 1
    if(grab_Checksum>=4):
         return 'Grab'

    pinch_Dis = Euclidean_Dis(thumb_tip,index_tip)
    pinch_Threshold = 50
    if(pinch_Dis<pinch_Threshold):
        return 'Pinch'
    
    return 'Default'