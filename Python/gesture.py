
import vector

def gestures(handsLM):
    wrist = handsLM[0]
    
    thumb_tip = handsLM[4]
    index_tip = handsLM[8]
    middle_tip = handsLM[12]
    ring_tip = handsLM[16]
    pinky_tip = handsLM[20]
    
    thumb_mcp = handsLM[1]
    index_mcp = handsLM[5]
    middle_mcp = handsLM[9]
    ring_mcp = handsLM[13]
    pinky_mcp = handsLM[17]

    hand_size = vector.Euclidean_Dis(middle_mcp, wrist)
    print(hand_size)

    fingerLen_List = [
        vector.Euclidean_Dis(thumb_tip, thumb_mcp),
        vector.Euclidean_Dis(index_tip, index_mcp),
        vector.Euclidean_Dis(middle_tip, middle_mcp),
        vector.Euclidean_Dis(ring_tip, ring_mcp),
        vector.Euclidean_Dis(pinky_tip, pinky_mcp)
    ]

    grab_List = [
        vector.Euclidean_Dis(thumb_tip, wrist),
        vector.Euclidean_Dis(index_tip, wrist),
        vector.Euclidean_Dis(middle_tip, wrist),
        vector.Euclidean_Dis(ring_tip, wrist),
        vector.Euclidean_Dis(pinky_tip, wrist)
    ]
    grab_Threshold = 150
    grab_Checksum = 0
    for grab in grab_List:
         if(grab<grab_Threshold):
              grab_Checksum += 1
    if(grab_Checksum>=4):
         return 'Grab'

    pinch_Dis = vector.Euclidean_Dis(thumb_tip,index_tip)
    pinch_Threshold = 50
    if(pinch_Dis<pinch_Threshold):
        return 'Pinch'
    
    return 'Default'