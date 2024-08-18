const net = require('net');

const client = new net.Socket();
const PORT = 6001;

let jsonObject = {
  "Id": "6",
  "OrderState":"New",
  "OrderType": "Enrollment1",
  "Major":"Computer Science",
  "course":"Java",
  "UserData": 
    {
      "name": "Hadi Hoteit",
      "age": 22,
      "oib":123,
      "dateOfBirth":"12-12-2000",
      "email":"hadi.hoteit1664@example.com"
    }
};


let jsonString = JSON.stringify(jsonObject);

client.connect(PORT, 'localhost', () => {
  console.log('Connected to server');
  client.write(jsonString);
  client.end();
});

client.on('data', data => {
  console.log('Received:', data.toString());
});

client.on('close', () => {
  console.log('Connection closed');
});
