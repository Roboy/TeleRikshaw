#!/usr/bin/env python
import rospy
import serial
from std_msgs.msg import String

arduinoSerialData =  serial.Serial('/dev/ttyACM0', 9600)

def callback(data):
    # TODO: write to serial port "S" + 0 ... 510
    arduinoSerialData.write(data.data + "\n")
    #rospy.loginfo(str(data.data) + "\n")
    
def speed_subscriber():

    rospy.init_node('speed_subscriber', anonymous=True)
    rospy.Subscriber("speed", String, callback)
    rospy.spin()

if __name__ == '__main__':
    speed_subscriber()