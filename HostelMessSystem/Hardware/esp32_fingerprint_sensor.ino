/*
  ESP32 Fingerprint Sensor Example (Adafruit Fingerprint Sensor)
  - Captures fingerprint ID
  - Sends POST /api/attendance/biometric
*/
#include <WiFi.h>
#include <HTTPClient.h>
#include <Adafruit_Fingerprint.h>

const char* ssid = "YOUR_WIFI_SSID";
const char* password = "YOUR_WIFI_PASSWORD";
const char* apiUrl = "http://YOUR_SERVER_HOST/api/attendance/biometric";
const char* jwtToken = "YOUR_STAFF_OR_ADMIN_JWT";

HardwareSerial mySerial(2);
Adafruit_Fingerprint finger = Adafruit_Fingerprint(&mySerial);

void setup() {
  Serial.begin(115200);
  mySerial.begin(57600, SERIAL_8N1, 16, 17);

  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  finger.begin(57600);
  if (finger.verifyPassword()) {
    Serial.println("Fingerprint sensor found");
  } else {
    Serial.println("Fingerprint sensor not found");
    while (1) { delay(1); }
  }
}

void loop() {
  int id = getFingerprintID();
  if (id <= 0) {
    delay(500);
    return;
  }

  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    http.begin(apiUrl);
    http.addHeader("Content-Type", "application/json");
    http.addHeader("Authorization", String("Bearer ") + jwtToken);

    String payload = "{\"biometricId\":\"BIO" + String(id) + "\"}";
    int httpCode = http.POST(payload);
    Serial.printf("POST Biometric response: %d\n", httpCode);
    Serial.println(http.getString());
    http.end();
  }

  delay(2000);
}

int getFingerprintID() {
  if (finger.getImage() != FINGERPRINT_OK) return -1;
  if (finger.image2Tz() != FINGERPRINT_OK) return -1;
  if (finger.fingerFastSearch() != FINGERPRINT_OK) return -1;

  Serial.print("Found ID #");
  Serial.println(finger.fingerID);
  return finger.fingerID;
}
