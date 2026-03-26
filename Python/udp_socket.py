import socket

address = '112.76.56.77'
send_port = 50000
recv_port = 50001
send_address = (address,send_port)
recv_address = (address,recv_port)


def send_udp(data):
    send = socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
    send.sendto(str.encode(str(data)), send_address)



def recv_udp():
    recv = socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
    recv.bind(recv_address)