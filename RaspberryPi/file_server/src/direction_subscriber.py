#!/usr/bin/env python
import rospy
import serial
from std_msgs.msg import Int16

arduinoSerialData =  serial.Serial('/dev/ttyACM0', 9600)

def callback(data):
    # TODO: write to serial port "D" + 40 ... 140
    arduinoSerialData.write("D" + str(data.data) + "\n")
    #rospy.loginfo(str(data.data) + "\n")
    
def direction_subscriber():

    rospy.init_node('direction_subscriber', anonymous=True)
    rospy.Subscriber("direction_car", Int16, callback)
    rospy.spin()

if __name__ == '__main__':
    direction_subscriber()