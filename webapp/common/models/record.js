module.exports = function(Record) {

  const HISTORY_LUCK = 3;


  Record.afterRemote('*.__updateById__sharing',
    function(ctx, player_affected, next) {

      // Check if the affected record still alive
      Record.app.models.Player.find(
        {
          where: {sharing_id: ctx.req.params.id}
        },
        function(err, players){
          if(err) next(err);

          // Destroy Record if nobody share it
          if(players.length == 0){
            Record.findById(
              ctx.req.params.id,
              function(err, r){
                if(err) return(err);

                r.history += 1;

                if(r.history > HISTORY_LUCK){
                  Record.destroyById(ctx.req.params.id)
                }else{
                  r.save();
                }
              }
            )
          }
        }
      )
      next();
    }
  );



  // Update author status
  Record.observe('before delete', function(context, next) {

    Record.findById(
      context.where.id,
      function(err, r){
        console.log('r', r);
        Record.app.models.Player.findById(
          r.author_id,
          function(err, p) {

            if(err) return(err);
            // TODO NOTIFY PLAYER HE DIES
            p.message_sent = false;
            p.save();
            console.log(context.Model.pluralModelName, context.where.id, 'DELETED');
            next();
          }
        );
      }
    )
  });

};
