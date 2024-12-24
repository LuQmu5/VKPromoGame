mergeInto(LibraryManager.library, {
  RequestJsFirstPromoUse: function () {
    firstPromoUse();
  },

  RequestJsSecondPromoUse: function () {
    secondPromoUse();
  },
  
    RequestJsCheckSubscribe: function () {
      checkSubscribe();
  },

  RequestJsGetPromo: function(str){
    getPromo(UTF8ToString(str));
  }
});
