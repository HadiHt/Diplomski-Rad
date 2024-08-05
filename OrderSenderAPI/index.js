const net = require('net');

const client = new net.Socket();
const PORT = 6001;

let jsonObject = {
  "Id": "4",
  "OrderState":"New",
  "OrderType": "Enrollment",
  "UserData": 
    {
      "name": "Hadi Hoteit",
      "age": 23
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
