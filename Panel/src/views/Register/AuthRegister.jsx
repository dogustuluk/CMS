import React from 'react';
import { useNavigate, Link as RouterLink } from 'react-router-dom';

// material-ui
import { useTheme } from '@mui/material/styles';
import {
    Box,
    Button,
    Divider,
    FormHelperText,
    Grid,
    TextField,
    Typography,
    FormControl,
    InputLabel,
    OutlinedInput,
    InputAdornment,
    IconButton,
    FormControlLabel,
    Checkbox
} from '@mui/material';

// third party
import * as Yup from 'yup';
import { Formik } from 'formik';

// assets
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
import Google from 'assets/images/social-google.svg';

import { useAuth } from '../../context/AuthContext';


// ==============================|| FIREBASE REGISTER ||============================== //

const AuthRegister = ({ ...rest }) => {
    const theme = useTheme();
    const [showPassword, setShowPassword] = React.useState(false);
    const [checked, setChecked] = React.useState(false);

    const navigate = useNavigate();
    const { registerUser } = useAuth();

    const handleClickShowPassword = () => {
        setShowPassword(!showPassword);
    };

    const handleMouseDownPassword = (event) => {
        event.preventDefault();
    };

    return (
        <>
            <Grid container justifyContent="center">
                <Grid item xs={12}>
                    <Button
                        fullWidth={true}
                        sx={{
                            fontSize: { md: '1rem', xs: '0.875rem' },
                            fontWeight: 500,
                            backgroundColor: theme.palette.grey[50],
                            color: theme.palette.grey[600],
                            textTransform: 'capitalize',
                            '&:hover': {
                                backgroundColor: theme.palette.grey[100]
                            }
                        }}
                        size="large"
                        variant="contained"
                    >
                        <img
                            src={Google}
                            alt="google"
                            width="20px"
                            style={{
                                marginRight: '16px',
                                '@media (maxWidth:900px)': {
                                    marginRight: '8px'
                                }
                            }}
                        />{' '}
                        Register with Google
                    </Button>
                </Grid>
            </Grid>

            <Box alignItems="center" display="flex" mt={2}>
                <Divider sx={{ flexGrow: 1 }} orientation="horizontal" />
                <Typography color="textSecondary" variant="h5" sx={{ m: theme.spacing(2) }}>
                    OR
                </Typography>
                <Divider sx={{ flexGrow: 1 }} orientation="horizontal" />
            </Box>

            <Formik
                initialValues=
                {{
                    username: '',
                    email: '',
                    password: '',
                    role: 0,
                    tenantName: '',
                    domain: '',
                }}
                validationSchema={Yup.object().shape({
                    username: Yup.string().required('Username is required'),
                    email: Yup.string().email('Invalid email').required('Email is required'),
                    password: Yup.string().min(6).required('Password is required'),
                    tenantName: Yup.string().required('Tenant name is required'),
                    domain: Yup.string().required('Domain name is required')
                })}
                onSubmit={async (values, { setSubmitting, setErrors }) => {
                    try {
                        await registerUser(values.username, values.email, values.password, values.role, values.tenantName, values.domain);
                        navigate('/');
                    }
                    catch (errors) {
                        setErrors({ submit: 'Registration failed. Please try again.' });
                    }
                    finally {
                        setSubmitting(false);
                    }
                }}
            >
                {({ handleSubmit, handleChange, values, errors, touched, isSubmitting }) => (
                    <form noValidate onSubmit={handleSubmit}>
                        <Grid container spacing={2}>
                            <Grid item xs={12}>
                                <TextField
                                    fullWidth
                                    label="Username"
                                    name="username"
                                    value={values.username}
                                    onChange={handleChange}
                                    error={touched.username && Boolean(errors.username)}
                                    helperText={touched.username && errors.username}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    fullWidth
                                    label="Email"
                                    name="email"
                                    value={values.email}
                                    onChange={handleChange}
                                    error={touched.email && Boolean(errors.email)}
                                    helperText={touched.email && errors.email}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    fullWidth
                                    label="Password"
                                    name="password"
                                    type="password"
                                    value={values.password}
                                    onChange={handleChange}
                                    error={touched.password && Boolean(errors.password)}
                                    helperText={touched.password && errors.password}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    fullWidth
                                    label="Tenant Name"
                                    name="tenantName"
                                    value={values.tenantName}
                                    onChange={handleChange}
                                    error={touched.tenantName && Boolean(errors.tenantName)}
                                    helperText={touched.tenantName && errors.tenantName}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    fullWidth
                                    label="Domain Name"
                                    name="domain"
                                    value={values.domain}
                                    onChange={handleChange}
                                    error={touched.domain && Boolean(errors.domain)}
                                    helperText={touched.domain && errors.domain}
                                />
                            </Grid>

                            <Grid item xs={12}>
                                <Button type="submit" variant="contained" fullWidth disabled={isSubmitting}>
                                    Register
                                </Button>
                            </Grid>
                        </Grid>
                    </form>
                )}
            </Formik>
        </>
    );
};

export default AuthRegister;
