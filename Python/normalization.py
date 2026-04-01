import vector

def Normalize(handsLM):
    wrist = handsLM[0]

    normalized = []
    for lm in handsLM:
        normalized.append([
            lm[0] - wrist[0],
            lm[1] - wrist[1],
            lm[2] - wrist[2]
        ])

    scale = vector.Euclidean_Dis(normalized[0], normalized[9])
    normalized = [[x/scale, y/scale, z/scale] for x,y,z in normalized]
    
    return normalized