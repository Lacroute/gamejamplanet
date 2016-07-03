module.exports = function(Record) {

  // send target_id null to go to transit
  Record.share = function(id, shared, cb) {
    Record.findOne(
      {
        where: { id: id }
      },
      function(err, rec){
        if(err) return cb(err);

        if(shared) {
          rec.echo_count += 1;
        }
        rec.shared = false;
        rec.save();
      }
    );
  };

  Record.remoteMethod(
    'share',
    {
      accepts: [
        {arg: 'id', type: 'number', required: true},
        {arg: 'shared', type: 'boolean'}
      ],
      returns: {},
      http: {path:'/:id/share', verb: 'post'}
    }
  );


  Record.observe('before delete', function(context, next) {
    Record.app.models.Player.findOne(
      { where: context.where }, function(err, player) {
        player.message_sent = false;
        player.save();
    });

    next();
  });

};
