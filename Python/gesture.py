import math

data = [
  {
    "frame": 1,
    "hand": "Right",
    "landmarks": [
      {"id": 0, "x": 0.52, "y": 0.78, "z": -0.02},
      {"id": 1, "x": 0.50, "y": 0.70, "z": -0.01},
      {"id": 2, "x": 0.48, "y": 0.62, "z": 0.00},
      {"id": 3, "x": 0.46, "y": 0.55, "z": 0.01},
      {"id": 4, "x": 0.44, "y": 0.50, "z": 0.02},

      {"id": 5, "x": 0.55, "y": 0.60, "z": -0.01},
      {"id": 6, "x": 0.57, "y": 0.50, "z": -0.02},
      {"id": 7, "x": 0.58, "y": 0.42, "z": -0.01},
      {"id": 8, "x": 0.59, "y": 0.35, "z": 0.00},

      {"id": 9, "x": 0.60, "y": 0.62, "z": -0.02},
      {"id": 10, "x": 0.62, "y": 0.52, "z": -0.01},
      {"id": 11, "x": 0.63, "y": 0.44, "z": 0.00},
      {"id": 12, "x": 0.64, "y": 0.36, "z": 0.01},

      {"id": 13, "x": 0.65, "y": 0.65, "z": -0.02},
      {"id": 14, "x": 0.67, "y": 0.55, "z": -0.01},
      {"id": 15, "x": 0.68, "y": 0.47, "z": 0.00},
      {"id": 16, "x": 0.69, "y": 0.40, "z": 0.01},

      {"id": 17, "x": 0.70, "y": 0.68, "z": -0.02},
      {"id": 18, "x": 0.72, "y": 0.60, "z": -0.01},
      {"id": 19, "x": 0.73, "y": 0.52, "z": 0.00},
      {"id": 20, "x": 0.74, "y": 0.45, "z": 0.01}
    ]
  },
   {
    "frame": 1,
    "hand": "Right",
    "landmarks": [
      {"id": 0, "x": 0.50, "y": 0.75, "z": 0.00},

      {"id": 1, "x": 0.48, "y": 0.68, "z": -0.01},
      {"id": 2, "x": 0.47, "y": 0.60, "z": -0.01},
      {"id": 3, "x": 0.46, "y": 0.53, "z": 0.00},
      {"id": 4, "x": 0.52, "y": 0.45, "z": 0.02},

      {"id": 5, "x": 0.55, "y": 0.60, "z": -0.01},
      {"id": 6, "x": 0.56, "y": 0.52, "z": -0.01},
      {"id": 7, "x": 0.57, "y": 0.47, "z": 0.00},
      {"id": 8, "x": 0.53, "y": 0.46, "z": 0.02},

      {"id": 9, "x": 0.60, "y": 0.62, "z": -0.02},
      {"id": 10, "x": 0.62, "y": 0.55, "z": -0.02},
      {"id": 11, "x": 0.63, "y": 0.50, "z": -0.01},
      {"id": 12, "x": 0.64, "y": 0.48, "z": 0.00},

      {"id": 13, "x": 0.65, "y": 0.65, "z": -0.02},
      {"id": 14, "x": 0.67, "y": 0.58, "z": -0.01},
      {"id": 15, "x": 0.68, "y": 0.53, "z": 0.00},
      {"id": 16, "x": 0.69, "y": 0.50, "z": 0.01},

      {"id": 17, "x": 0.70, "y": 0.68, "z": -0.02},
      {"id": 18, "x": 0.72, "y": 0.62, "z": -0.01},
      {"id": 19, "x": 0.73, "y": 0.57, "z": 0.00},
      {"id": 20, "x": 0.74, "y": 0.54, "z": 0.01}
    ]
  },
  {
  "frame": 1,
  "hand": "Right",
  "landmarks": [
    {"id": 0, "x": 0.50, "y": 0.75, "z": 0.00},

    {"id": 1, "x": 0.48, "y": 0.72, "z": -0.02},
    {"id": 2, "x": 0.47, "y": 0.74, "z": -0.02},
    {"id": 3, "x": 0.48, "y": 0.76, "z": -0.01},
    {"id": 4, "x": 0.50, "y": 0.77, "z": 0.00},

    {"id": 5, "x": 0.52, "y": 0.72, "z": -0.02},
    {"id": 6, "x": 0.53, "y": 0.75, "z": -0.02},
    {"id": 7, "x": 0.52, "y": 0.77, "z": -0.01},
    {"id": 8, "x": 0.51, "y": 0.78, "z": 0.00},

    {"id": 9, "x": 0.55, "y": 0.72, "z": -0.02},
    {"id": 10, "x": 0.56, "y": 0.75, "z": -0.02},
    {"id": 11, "x": 0.55, "y": 0.77, "z": -0.01},
    {"id": 12, "x": 0.54, "y": 0.78, "z": 0.00},

    {"id": 13, "x": 0.58, "y": 0.73, "z": -0.02},
    {"id": 14, "x": 0.59, "y": 0.75, "z": -0.02},
    {"id": 15, "x": 0.58, "y": 0.77, "z": -0.01},
    {"id": 16, "x": 0.57, "y": 0.78, "z": 0.00},

    {"id": 17, "x": 0.61, "y": 0.74, "z": -0.02},
    {"id": 18, "x": 0.62, "y": 0.76, "z": -0.02},
    {"id": 19, "x": 0.61, "y": 0.78, "z": -0.01},
    {"id": 20, "x": 0.60, "y": 0.79, "z": 0.00}
  ]
}
]
default_Hand = data[0]['landmarks']
pinch_Hand = data[1]['landmarks']
grab_Hand = data[2]['landmarks']

def Euclidean_Dis(coordinate_0,coordinate_1):
    return math.sqrt(
        (coordinate_0['x'] - coordinate_1['x'])**2 +
        (coordinate_0['y'] - coordinate_1['y'])**2 +
        (coordinate_0['z'] - coordinate_1['z'])**2
    )*1000

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
    grab_Threshold = 100
    grab_Checksum = 0
    for grab in grabList:
         if(grab<grab_Threshold):
              grab_Checksum += 1
    if(grab_Checksum>=4):
         return 'Grab'

    pinch_Dis = Euclidean_Dis(thumb_tip,index_tip)
    pinch_Threshold = 20
    if(pinch_Dis<pinch_Threshold):
        return 'Pinch'

    

print(gestures(default_Hand))
print(gestures(pinch_Hand))
print(gestures(grab_Hand))