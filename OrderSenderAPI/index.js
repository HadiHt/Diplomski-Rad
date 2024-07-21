const net = require('net');

const client = new net.Socket();
const PORT = 6001;

client.connect(PORT, 'localhost', () => {
  console.log('Connected to server');
  client.write('Hello, Node.js!');
  client.end();
});

client.on('data', data => {
  console.log('Received:', data.toString());
});

client.on('close', () => {
  console.log('Connection closed');
});
