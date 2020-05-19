import React, { useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import {
  Paper,
  Tabs,
  Tab
} from "@material-ui/core";
import TicketForm from "./components/TicketForm";
import TicketList from "./components/TicketList";

const useStyles = makeStyles(() => ({
  menu: {
    flexGrow: 1,
  }
}));

const App = () => {
  const classes = useStyles();

  const [value, setValue] = useState(0);

  const handleChange = (event, newValue) => {
    setValue(newValue);
  };

  return (
    <div className="App">
      <Paper square className={classes.menu}>
        <Tabs
          value={value}
          onChange={handleChange}
          indicatorColor="primary"
          textColor="primary"
          centered
        >
          <Tab label="New Ticket" />
          <Tab label="Tickets List" />
        </Tabs>
      </Paper>
      {value === 0 && (<TicketForm />)}
      {value === 1 && (<TicketList />)}

    </div>
  );
}

export default App;
