$(function(){
  // setup autocomplete function pulling from currencies[] array
  $('#inputSearch').autocomplete({
    lookup: currencies,
    onSelect: function (suggestion) {
        //alert(suggestion.data);
        location.href = "/Pages/Search.aspx?key=" + suggestion.data;
    }
  });
  

});
