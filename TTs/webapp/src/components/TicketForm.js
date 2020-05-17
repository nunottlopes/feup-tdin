import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import {
    Container,
    Grid,
    TextField,
    Button
} from "@material-ui/core";
import { sendTicket } from "../ApiServices.js";


const useStyles = makeStyles(() => ({
    form: {
        margin: "1em"
    },

    submit: {
        marginTop: "1em",
    }
}));

const TicketForm = () => {
    const classes = useStyles();

    const [state, setState] = React.useState({
        name: "",
        email: "",
        title: "",
        description: ""
    });

    const handleChange = (event) => {
        setState({ ...state, [event.target.name]: event.target.value });
    }

    const handleSubmit = (event) => {
        event.preventDefault();
        console.log(state);
        sendTicket(state);
    }

    return (
        <Container component="main">
            <form onSubmit={handleSubmit} className={classes.form}>
                <Grid container spacing={2}>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            name="name"
                            variant="outlined"
                            required
                            fullWidth
                            id="name"
                            label="Name"
                            autoFocus
                            onChange={handleChange}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            variant="outlined"
                            required
                            fullWidth
                            id="email"
                            label="Email"
                            name="email"
                            type="email"
                            onChange={handleChange}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            variant="outlined"
                            required
                            fullWidth
                            id="title"
                            label="Title"
                            name="title"
                            onChange={handleChange}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            variant="outlined"
                            required
                            fullWidth
                            multiline
                            rows={3}
                            name="description"
                            label="Description"
                            id="description"
                            onChange={handleChange}
                        />
                    </Grid>
                </Grid>
                <Button
                    type="submit"
                    variant="contained"
                    color="primary"
                    className={classes.submit}
                >
                    Submit
                </Button>
            </form>
        </Container>
    );
}

export default TicketForm;