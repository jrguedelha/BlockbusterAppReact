//import logo from './logo.svg';
import './App.css';

import {Home} from './Home';
import {Movie} from './Movie';
import {Genre} from './Genre';
import {Navigation} from './Navigation';

import {BrowserRouter, Route, Switch} from 'react-router-dom';

function App() {
  return (
    <BrowserRouter>
      <div className="container">
        <h3 className="m-3 d-flex justify-content-center">
          Blockbuster
        </h3>
        
        <Navigation/>

        <Switch>
          <Route path='/' component={Home} exact/>
          <Route path='/movie' component = {Movie}/>
          <Route path='/genre' component = {Genre}/>
        </Switch>
      </div>
    </BrowserRouter>
  );
}

export default App;