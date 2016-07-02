module.exports = function(Record) {

  Record.updateTarget = function(id, new_target_id, cb) {
    Record.findOne({where:{id:id}}, function(err, rec){
      rec.target_id = new_target_id;
      rec.echo_count += 1;
      rec.save();
    });

    cb();
  };
  Record.remoteMethod(
    'updateTarget',
    {
      accepts: [
        {arg: 'id', type: 'number', required: true},
        {arg: 'new_target_id', type: 'number'}],
      returns: {},
      http: {path:'/:id/updateTarget', verb: 'post'}
    }
  );

};
