mergeInto(LibraryManager.library, {

  RequestJsCheckSubscribe: function () {
    checkSubscribe();
  },

  RequestJsCheckPostStory: function(){
    сheckPostStory();
  },

  RequestJsCheckPromocodeSend: function(str){
    сheckPromocodeSend(UTF8ToString(str));
  }
});
