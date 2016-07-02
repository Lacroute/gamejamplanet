module.exports = function(Player) {

  // Player.afterRemote('**', function(ctx, inst, next) {
  //   console.log('Player', 'ctx.methodString', ctx.methodString);
  //
  //   next();
  // });
  Player.afterCreate = function(next) {
    Player.app.models.Pendingbox.create({handle_id: this.id}, next);
  }

  Player.afterRemote('*.__create__author', function(ctx, inst, next) {
    Player.findOne({where:{id:inst.author_id}}, function(err, res){
      res.message_count ++;
      res.message_sent = true;
      res.save();
    });

    Player.app.models.Record.findOne(
      {where:{id:inst.id}},function(err, res){
      res.target_id = res.author_id;
      res.save();
    });

    next();
  });


};
