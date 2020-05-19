import React from 'react';
import Main from './pages/Main';
import Login from './pages/Login';
import Secondary from './pages/Secondary';

function App() {

  const Views = () => {
    return {
      main: <Main />,
      login: <Login />,
      secondary: <Secondary />
    }
  }
  let name = window.location.search.substr(1);
  let view = Views()[name];

  if(view == null)
    throw new Error("View is undefined");

  return view;
}

export default App;
