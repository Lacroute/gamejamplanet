module.exports = function(Player) {

  Player.afterRemote('*.__create__author', function(ctx, inst, next) {
    Player.findOne({where:{id:inst.author_id}}, function(err, res){
      res.message_count ++;
      res.save();
    })

    next();
  });


};
