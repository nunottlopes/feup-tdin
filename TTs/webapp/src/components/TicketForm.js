import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import {
    Container,
    Grid,
    TextField,
    Button,
    Snackbar,
} from "@material-ui/core";
import {
    Alert
} from "@material-ui/lab";
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

    const [snackbarOpen, setSnackbarOpen] = React.useState({
        open: false,
        message: "",
        severity: ""
    });

    const handleChange = (event) => {
        setState({ ...state, [event.target.name]: event.target.value });
    }

    const snackbarClose = (event) => {
        setSnackbarOpen({ open: false });
    }

    const handleSubmit = async (event) => {
        event.preventDefault();
        console.log(state);
        const res = await sendTicket(state);
        if (res.status === 201) {
            setSnackbarOpen({ open: true, message: "Ticket submited!", severity: "success" });
        } else {
            setSnackbarOpen({ open: true, message: "Ticket submission failed!", severity: "error" })
        }
    }

    return (
        <>
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
            <Snackbar
                open={snackbarOpen.open}
                autoHideDuration={6000}
                onClose={snackbarClose}
            >
                <Alert onClose={snackbarClose} severity={snackbarClose.severity}>
                    {snackbarOpen.message}
                </Alert>
            </Snackbar>
        </>

    );
}

export default TicketForm;