const cluster = require('cluster');
const net = require('net');
const amqp = require('amqplib');

const PORT = 6001;
const RABBITMQ_URL = 'amqp://localhost:5672'; // Correct RabbitMQ port

console.log("Starting Receiver!");

if (cluster.isMaster) {
  const numCPUs = require('os').cpus().length;
  console.log('Number of CPUs: ' + numCPUs);
  console.log(`Master ${process.pid} is running`);

  for (let i = 0; i < numCPUs; i++) {
    cluster.fork();
  }

  cluster.on('exit', (worker, code, signal) => {
    console.log(`Worker ${worker.process.pid} died`);
  });
} else {
  console.log(`Worker ${process.pid} started`);

  // Create a RabbitMQ connection and channel with retry logic
  async function createRabbitMQChannel(retries = 5) {
    try {
      const connection = await amqp.connect(RABBITMQ_URL);
      const channel = await connection.createChannel();
      await channel.assertQueue('Order_Receiver_API', { durable: true });
      return channel;
    } catch (error) {
      if (retries > 0) {
        console.error(`Failed to create RabbitMQ channel: ${error}. Retrying...`);
        await new Promise(resolve => setTimeout(resolve, 5000)); // Wait 5 seconds before retrying
        return createRabbitMQChannel(retries - 1);
      } else {
        console.error(`Failed to create RabbitMQ channel after multiple attempts: ${error}`);
        process.exit(1);
      }
    }
  }

  let rabbitMQChannel;
  createRabbitMQChannel().then(channel => {
    rabbitMQChannel = channel;

    const server = net.createServer(socket => {
      let data = '';

      socket.on('data', chunk => {
        data += chunk.toString();
      });

      socket.on('end', () => {
        if (rabbitMQChannel) {
          rabbitMQChannel.sendToQueue('Order_Receiver_API', Buffer.from(data), {
            persistent: true,
          });
        }
        socket.end();
      });

      socket.on('error', err => {
        console.error(`Socket error: ${err}`);
      });
    });

    server.listen(PORT, () => {
      console.log(`Worker ${process.pid} listening on port ${PORT}`);
    });

    server.on('error', err => {
      console.error(`Server error: ${err}`);
    });
  }).catch(err => {
    console.error(`Failed to create RabbitMQ channel: ${err}`);
  });
}
