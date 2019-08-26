var net = require('net');

var allClients = [];

var server = net.createServer(function(socket) {
	socket.pipe(socket);
});

var os = require('os');
var ifaces = os.networkInterfaces();

var internalAddress = "";

Object.keys(ifaces).forEach(function (ifname) {
    var alias = 0;

    ifaces[ifname].forEach(function (iface) {
        if ('IPv4' !== iface.family || iface.internal !== false) {
            // skip over internal (i.e. 127.0.0.1) and non-ipv4 addresses
            return;
        }

        if (alias >= 1) {
            // this single interface has multiple ipv4 addresses
            console.log(ifname + ':' + alias, iface.address);
            internalAddress = iface.address.toString();
        } else {
            // this interface has only one ipv4 adress
            console.log(ifname, iface.address);
            internalAddress = iface.address.toString();
        }
        ++alias;
    });
});

var playerCount = process.argv[2];
server.listen(35555, internalAddress);

console.log("Server running on " + internalAddress + "! Min Player Count: " + playerCount);

server.on("connection", function (socket) {
    console.log("New Client " + socket.remoteAddress);
    allClients.push(socket);
    if (allClients.length >= playerCount) {
        allClients.forEach((s) => {
            s.write("%BeginGame");
        });
    }
    socket.on("data", function (data) {
        var s = data.toString("UTF8");
        console.log(s);
        var command = s.split(" ");
        if (command[0] == "$SendBlocker") {
            allClients.forEach(function (s) {
                if (s.remoteAddress != socket.remoteAddress) {
                    s.write("%RecieveBlocker " + command[1]);
                }
            });
        }
        if (command[0] == "$LostGame") {
            console.log("Client disconnected " + socket.remoteAddress + " " + allClients.length);
            allClients = allClients.filter(function (ss) {
                return ss.remoteAddress != socket.remoteAddress;
            });
            if (allClients.length <= 1 && allClients.length > 0) {

                allClients[0].write("%GameWon");
                allClients[0].destroy();
                console.log("Game Over!");
            }
            socket.destroy();
        }
    });
});
