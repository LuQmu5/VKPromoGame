mergeInto(LibraryManager.library, {

  RequestJsSubscribe: function () {
    subscribe();
  },

  RequestJsPostStory: function(){
    postStory();
  },

  RequestJsPromocodeSend: function(str){
    promocodeSend(UTF8ToString(str));
  }
});
