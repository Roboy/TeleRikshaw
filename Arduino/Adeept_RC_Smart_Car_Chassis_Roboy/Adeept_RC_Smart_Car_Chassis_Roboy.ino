#include <SPI.h>
#include <Servo.h>
#include <ros.h>
#include <std_msgs/Int16.h>
#include <std_msgs/Float32.h>

Servo dirServo;                                               // defines servo to control turning of car
int dirServoPin = 2;                                          // defines pin for signal line of the last servo
int dirServoOffset = 8;                                       // defines a variable for deviation (degree) of the servo

Servo ultrasonicServo;                                        // define servo to control turning of ultrasonic sensor
int ultrasonicPin = 3;                                        // define pin for signal line of the last servo
int ultraServoOffset = 2;                                       // defines a variable for deviation (degree) of the cam servo
int trigPin = 0;                                              // define Trig pin for ultrasonic ranging module
int echoPin = 1;                                              // define Echo pin for ultrasonic ranging module

#define FORWARD HIGH
#define BACKWARD LOW

const int dirAPin = 7;                                        // define pin used to control rotational direction of motor A
const int pwmAPin = 6;                                        // define pin for PWM used to control rotational speed of motor A
const int dirBPin = 4;                                        // define pin used to control rotational direction of motor B
const int pwmBPin = 5;                                        // define pin for PWM used to control rotational speed of motor B
const int buzzerPin = 8;                                      // define pin for buzzer
const int RPin = A3; 
const int GPin = A4; 
const int BPin = A5; 
char ctrl;                                                    // control variable (D=Direction, C=Camera Direction)
const int MaxChars = 5;                                       // maximum numbers of serial input chars 5 (1 control variable+ 4 for max int input
char strValue[MaxChars+1];                                    // array for chars for serial input after control variable
int serialInt = 0;                                            // int for serial input (strValue) read after control variable
int index = 0;                                                // index for strValue

//Definition for Jingle Bells

int speakerPin = 8;                                           // same as buzzer pin
int length = 26;
char notes[] = "eeeeeeegcde fffffeeeeddedg";
int beats[] = { 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2};
int tempo = 300;

float wheel_radius = 0.03;
float rpm = 0.0;
float speed_mps = 0.0;
float speed_kmh = 0.0;
const float Pi = 3.14159;

//Instatiate node handle for ROSserial communication
ros::NodeHandle nh;

//ROS publisher
std_msgs::Float32 speed_msg;
ros::Publisher pub_speed("speed_stan", &speed_msg);

//Subscriber callback functions
void directionCb( const std_msgs::Int16& msg){
  carDirectionD(msg.data);
}

void speedCb( const std_msgs::Int16& msg){
  speedS(msg.data);
  // calculate speed in kmh
  rpm = (200.0/255.0) * msg.data - 200.0;
  speed_mps = (2.0 * Pi * wheel_radius * rpm) / 60.0;
  speed_kmh = speed_mps * 3.6;
  speed_msg.data = speed_kmh;
  pub_speed.publish( &speed_msg);
}

void musicCb( const std_msgs::Int16& msg){
  jingleBellsJ(msg.data);
}

void buzzerCb( const std_msgs::Int16& msg){
  buzzB(msg.data);
}

//ROS subscriber
ros::Subscriber<std_msgs::Int16> sub_direction("direction_car", &directionCb );
ros::Subscriber<std_msgs::Int16> sub_speed("speed_car", &speedCb );
ros::Subscriber<std_msgs::Int16> sub_music("music", &musicCb );
ros::Subscriber<std_msgs::Int16> sub_music("buzzer", &buzzerCb );

// car will run this instruction once when turned on 
void setup() {
  //Serial.begin(9600);
  
  dirServo.attach(dirServoPin);                               // attaches the servo on servoDirPin to the servo object
  dirServo.write(90-dirServoOffset);                                         // moves dirServo to 90 deg position (center)
  ultrasonicServo.attach(ultrasonicPin);                      // attaches the servo on ultrasonicPin to the servo object
  ultrasonicServo.write(90-ultraServoOffset);                                  // moves ultrasonicServo to 90 deg position (center)
  
  pinMode(dirAPin, OUTPUT);                                   // sets dirAPin to output mode
  pinMode(pwmAPin, OUTPUT);                                   // sets pwmAPin to output mode
  pinMode(dirBPin, OUTPUT);                                   // sets dirBPin to output mode
  pinMode(pwmBPin, OUTPUT);                                   // sets pwmBPin to output mode
  pinMode(buzzerPin, OUTPUT);                                 // sets buzzerPin to output mode
  pinMode(RPin, OUTPUT);                                      // sets RPin to output mode
  pinMode(GPin, OUTPUT);                                      // sets GPin to output mode
  pinMode(BPin, OUTPUT);                                      // sets BPin to output mode
  pinMode(trigPin, OUTPUT);                                   // sets trigPin to output mode
  pinMode(echoPin, INPUT);                                    // sets echoPin to input mode

  //initialize ROS node handle and subscribers
  nh.initNode();
  nh.subscribe(sub_direction);
  nh.subscribe(sub_speed);
  nh.subscribe(sub_music);
  nh.advertise(pub_speed);
  
}

// function to change car direction
void carDirectionD(int DInt)
{      
    if(DInt > 44 && DInt < 136){
    DInt -= dirServoOffset;
    dirServo.write(DInt); 
    }
}

// function to change cam direction
void camDirectionC(int CInt)
{       
    if(CInt > 44 && CInt < 136){
    CInt -= ultraServoOffset;
    ultrasonicServo.write(CInt);
    }
}  

// function to make noise
void buzzB(int BInt)
{       
    if(BInt > 0 && BInt < 2001){
    tone(buzzerPin, BInt, 500);
    }
} 

// function to play tone for jingle bells
void playTone(int tone, int duration) {
  for (long i = 0; i < duration * 1000L; i += tone * 2) {
    digitalWrite(speakerPin, HIGH);
    delayMicroseconds(tone);
    digitalWrite(speakerPin, LOW);
    delayMicroseconds(tone);
  }
}

// function to play a note for jingle bells
void playNote(char note, int duration) {
  char names[] = { 'c', 'd', 'e', 'f', 'g', 'a', 'b', 'C' };
  int tones[] = { 1915, 1700, 1519, 1432, 1275, 1136, 1014, 956 };
  
  // play the tone corresponding to the note name
  for (int i = 0; i < 8; i++) {
    if (names[i] == note) {
      playTone(tones[i], duration);
    }
  }
}

// function to make jingle Bells
void jingleBellsJ(int JInt)
{  
  for (int j = 0; j < JInt; j++) {     
  for (int i = 0; i < length; i++) {
    if (notes[i] == ' ') {
      delay(beats[i] * tempo); // rest
    } else {
      playNote(notes[i], beats[i] * tempo);
    }
    
    // pause between notes
    delay(tempo / 2); 
  }
  } 
}


// function to speed up (0-254 reverse, 255 stop, 256-510 forward)
void speedS(int SInt)
{       
    if(SInt > -1 && SInt < 511){
      SInt -= 255;
      bool motorDir = SInt > 0 ? FORWARD : BACKWARD;
      SInt = abs(constrain(SInt, -255, 255));
      digitalWrite(dirAPin, motorDir);                            // defines direction of drive motor A rotation (forward / reverse)
      digitalWrite(dirBPin, motorDir);                            // defines direction of drive motor B rotation (forward / reverse)
      analogWrite(pwmAPin, SInt);                                 // defines speed of drive motor A rotation (0 - 255) based on remote control joystick U1 position
      analogWrite(pwmBPin, SInt);                                 // defines speed of drive motor B rotation (0 - 255) based on remote control joystick U1 position
    }
} 

// function to Loop LED lights
void LEDloop() {
  for (int i=0; i <= 6; i++){ 
    switch (i) {
      case 0: digitalWrite(RPin, LOW);digitalWrite(GPin, LOW);digitalWrite(BPin, LOW); break;     //White
      case 1: digitalWrite(RPin, LOW);digitalWrite(GPin, LOW);digitalWrite(BPin, HIGH); break;    //Yellow
      case 2: digitalWrite(RPin, LOW);digitalWrite(GPin, HIGH);digitalWrite(BPin, LOW); break;    //Magenta
      case 3: digitalWrite(RPin, LOW);digitalWrite(GPin, HIGH);digitalWrite(BPin, HIGH); break;   //Red
      case 4: digitalWrite(RPin, HIGH);digitalWrite(GPin, LOW);digitalWrite(BPin, LOW); break;    //Cyan
      case 5: digitalWrite(RPin, HIGH);digitalWrite(GPin, LOW);digitalWrite(BPin, HIGH); break;   //Green
      case 6: digitalWrite(RPin, HIGH);digitalWrite(GPin, HIGH);digitalWrite(BPin, LOW); break;   //Blue
      case 7: digitalWrite(RPin, HIGH);digitalWrite(GPin, HIGH);digitalWrite(BPin, HIGH); break;  //Off
      default: break;
      }
     delay(100);
   }
}

// car will loop through this instruction continuously after turned on 
void loop() {  
   // loops though 7 LED colors for awesome looks
   LEDloop();
   nh.spinOnce();
   delay(1);
}

 
