int number;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  number = 0;
  
}

void loop() {
  // put your main code here, to run repeatedly:
   Serial.println(number);
   delay(1);
   if ((number<1000) && (number>-1)){
    number+=1;
   } 
   else {
    number=0;   
   }
   
}
