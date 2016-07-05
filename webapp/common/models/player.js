module.exports = function(Player) {

  // Player.afterRemote('**', function(ctx, inst, next) {
  //   console.log('Player', 'ctx.methodString', ctx.methodString);
  //
  //   next();
  // });


  // Link a player to his record.
  Player.afterRemote('*.__create__author', function(ctx, inst, next) {
    Player.findById(
      inst.author_id,
      function(err, res){
        res.message_count ++;
        res.message_sent = true;

        // First Record, init sharing with your id, otherwise continue to share the old one.
        if(!res.sharing_id){
          res.sharing_id = inst.id;
        }

        res.save();
      }
    );

    next();
  });


  // Pick up a Record in the pool
  Player.listenToSpace = function(id, cb) {
    Player.app.models.Record.find(
      {
        where: { author_id: {neq: id} }
      },
      function(err, recs){
        if(err) return cb(err);
        // Get Random using Math.floor(Math.random() * (max - min +1)) + min;
        r = recs[Math.floor(Math.random() * recs.length)];
        cb(null, r);
      }
    );
  };

  Player.remoteMethod(
    'listenToSpace',
    {
      accepts: [
        {arg: 'id', type: 'number', required: true}
      ],
      returns: {arg: 'record', type: 'Object'},
      http: {path:'/:id/listenToSpace', verb: 'get'}
    }
  );



  // Pick up a Record in the pool
  Player.shareRecord = function(id_player, id_new_record, cb) {
    Player.findById(
      id_player,
      function(err, p){
        if(err) return cb(err);
        
        p.sharing_id = id_new_record;
        cb(null, p);
      }
    );
  };

  Player.remoteMethod(
    'shareRecord',
    {
      accepts: [
        {arg: 'id_player', type: 'number', required: true},
        {arg: 'id_new_record', type: 'number', required: true}
      ],
      returns: {arg: 'player', type: 'Object'},
      http: {path:'/:id_player/shareRecord', verb: 'post'}
    }
  );

};
