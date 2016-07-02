var async = require('async');
module.exports = function(app) {

  //create all models
  async.parallel({
    players: async.apply(createPlayers),
    pendingboxes: async.apply(createPendingboxes),
  }, function(err, results) {
    if (err) throw err;
    cleanRecords(function(err){
      console.log('> models created sucessfully');
    })
    // createRecords(results.players, results.pendingboxes, function(err) {
    // console.log('> models created sucessfully');
    // });
  });


  //create players
  function createPlayers(cb) {
      var Player = app.models.Player;

      Player.create([
        {"message_sent": false, "message_count": 0},
        {"message_sent": false, "message_count": 0}
      ], cb);
  }


  //create pendingboxes
  function createPendingboxes(cb) {
      var Pendingbox = app.models.Pendingbox;
      Pendingbox.create([
        {pendingboxId: 1},
        {pendingboxId: 2},
      ], cb);
  }


  //create records
  function createRecords(players, pendingboxes, cb) {

      var Record = app.models.Record;
      Record.create([
        {
          "data": "Message from 1",
          "author_id": players[0].id,
        }
      ], cb);
  }


  // destroy function
  function cleanRecords(cb){
    var Record = app.models.Record;
    Record.destroyAll(cb);
  }
};
