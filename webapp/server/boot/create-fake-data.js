var async = require('async');
module.exports = function(app) {
  var creation_needed = false;

  if(creation_needed){

    //create all models
    async.parallel({
      players: async.apply(createPlayers)
    }, function(err, results) {
      if (err) throw err;
      // cleanRecords(function(err){
        // console.log('> models created sucessfully');
      // })
      createRecords(results.players, results.pendingboxes, function(err, results) {
        updatePendings(results, function(err){
          console.log('> models created sucessfully');
        })
      });
      // console.log('> models created sucessfully');
    });


    //create players
    function createPlayers(cb) {
        var Player = app.models.Player;

        Player.create([
          {"message_sent": true, "message_count": 0},
          {"message_sent": true, "message_count": 0},
          {"message_sent": true, "message_count": 0}
        ], cb);
    }


    //create records
    function createRecords(players, pendingboxes, cb) {

        var Record = app.models.Record;
        Record.create([
          {
            "data": "Message from " + players[0].id,
            "author_id": players[0].id,
          },
          {
            "data": "Message from " + players[1].id,
            "author_id": players[1].id,
          },
          {
            "data": "Message from " + players[2].id,
            "author_id": players[2].id,
          }
        ], cb);
    }

    // destroy function
    function updatePendings(records, cb){
      var Record = app.models.Record;

      for (var i = 0; i < records.length; i++) {
        Record.findOne({where: {id:records[i].id}}, function(err, rec){
          rec.updateAttributes({target_id: 1});
        });
      }
      cb();
    }


    // destroy function
    function cleanRecords(cb){
      var Record = app.models.Record;
      Record.destroyAll(cb);
    }
  }
};
