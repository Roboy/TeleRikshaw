import serial

ser = serial.Serial('/dev/cu.usbmodem14101',9600)  #use the serial port of the rasberry - arduino connection
while True:
    ser.write('D70')



#s = [0]
#read_serial=ser.readline()
#s[0] = str(int (ser.readline(),16))
#print s[0]
#print read_serial