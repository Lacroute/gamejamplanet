module.exports = function(Player) {

  // Player.afterRemote('**', function(ctx, inst, next) {
  //   console.log('Player', 'ctx.methodString', ctx.methodString);
  //
  //   next();
  // });


  // Link a player to his record.
  Player.afterRemote('*.__create__author', function(ctx, inst, next) {
    Player.findOne({where:{id:inst.author_id}}, function(err, res){
      res.message_count ++;
      res.message_sent = true;
      res.save();
    });

    Player.app.models.Record.findOne(
      {where:{id:inst.id}},function(err, res){
      res.target_id = null;
      res.save();
    });

    next();
  });


  // Pick up a Record in the pool
  Player.listenSpace = function(id, cb) {
    Player.app.models.Record.find(
      {
        where:{author_id: {neq: id}}
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
    'listenSpace',
    {
      accepts: [
        {arg: 'id', type: 'number', required: true}
      ],
      returns: {arg: 'record', type: 'Object'},
      http: {path:'/:id/listenSpace', verb: 'get'}
    }
  );


};
