var express = require('express');
var http = require('http');
var app = express();
var request = require('request');
var Sqlite3 = require('better-sqlite3');

var db = new Sqlite3('./data/test.db', {
	fileMustExist: true
});

app.use(express.json());

http.createServer(app).listen(8080);

app.get('/api/message/:id', function(req, res) {

	var id = req.params.id;

	if (id == undefined) {
		res.sendStatus(404).end();
	}

	var text = db.prepare("SELECT text FROM message WHERE messageId = ?").get(id).text;

	res.status(200).send({
		message: text
	});

});

app.post('/api/message', function(req, res) {

	console.log(req.body);

	if (req.body.message == undefined) {
		res.sendStatus(400).end();
	}

	var result = db.prepare("INSERT INTO message (text) VALUES (?)").run(req.body.message);
	var id = result.lastInsertRowid;
	res.location('/api/message/' + id).sendStatus(201).end();

});
