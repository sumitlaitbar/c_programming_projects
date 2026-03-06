/*
  ESP32 NFC Reader Example
  - Reads NFC UID
  - Sends POST /api/attendance/nfc
*/
#include <WiFi.h>
#include <HTTPClient.h>
#include <SPI.h>
#include <MFRC522.h>

#define SS_PIN 5
#define RST_PIN 22

const char* ssid = "YOUR_WIFI_SSID";
const char* password = "YOUR_WIFI_PASSWORD";
const char* apiUrl = "http://YOUR_SERVER_HOST/api/attendance/nfc";
const char* jwtToken = "YOUR_STAFF_OR_ADMIN_JWT";

MFRC522 nfc(SS_PIN, RST_PIN);

void setup() {
  Serial.begin(115200);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("WiFi connected");

  SPI.begin();
  nfc.PCD_Init();
}

void loop() {
  if (!nfc.PICC_IsNewCardPresent() || !nfc.PICC_ReadCardSerial()) {
    delay(200);
    return;
  }

  String uid = "";
  for (byte i = 0; i < nfc.uid.size; i++) {
    uid += String(nfc.uid.uidByte[i], HEX);
  }
  uid.toUpperCase();

  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    http.begin(apiUrl);
    http.addHeader("Content-Type", "application/json");
    http.addHeader("Authorization", String("Bearer ") + jwtToken);

    String payload = "{\"nfcCardId\":\"" + uid + "\"}";
    int httpCode = http.POST(payload);
    Serial.printf("POST NFC response: %d\n", httpCode);
    Serial.println(http.getString());
    http.end();
  }

  nfc.PICC_HaltA();
  delay(1500);
}
