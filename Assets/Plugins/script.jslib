mergeInto(LibraryManager.library, {
  RequestJsFirstPromoUse: function () {
    callFirstPromoFuncJS();
  },

  RequestJsSecondPromoUse: function () {
    callSecondPromoFuncJS();
  },

  RequestJsSubscribe: function () {
    callSubscribeFuncJS();
  },

    RequestJsCheckSubscribe: function () {
    callCheckSubscribeFuncJS();
  },
});
