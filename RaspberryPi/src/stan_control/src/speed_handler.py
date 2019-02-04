#!/usr/bin/env python
import rospy
import serial
from std_msgs.msg import String
import math

speed_command = 255
rpm = 0.0
speed_mps = 0.0
speed_kmh = 0.0
wheel_radius = 0.03

arduinoSerialData =  serial.Serial('/dev/ttyACM0', 9600)
pub = rospy.Publisher('stan_speed', String, queue_size=10)

def callback(data):
    arduinoSerialData.write(data.data + "\n")
    speed_command = int(data.data[1:])
    rpm = (200.0/255.0) * float(speed_command) - 200.0;
    # velocity in m/s = 2πrRPM/60
    speed_mps = (2 * math.pi * wheel_radius * rpm) / 60
    speed_kmh = speed_mps * 3.6
    pub.publish(speed_kmh)
    
def speed_handler():
    rospy.init_node('speed_handler_stan', anonymous=True)
    rospy.Subscriber("speed", String, callback)
    rospy.spin()

if __name__ == '__main__':
    try:
        speed_handler()
    except rospy.ROSInterruptException:
        pass
