import React from 'react';
import Main from './pages/Main';
import Login from './pages/Login';

// const {app} = window.require('electron').remote;

function App() {

  const Views = () => {
    return {
      main: <Main />,
      login: <Login />
    }
  }
  let name = window.location.search.substr(1);
  let view = Views()[name];

  if(view == null)
    throw new Error("View is undefined");

  return view;
}

export default App;
