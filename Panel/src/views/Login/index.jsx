import React, { useState } from 'react';
import { useTheme } from '@mui/material/styles';
import {
    Box,
    Button,
    FormHelperText,
    Grid,
    TextField,
    Typography,
    FormControl,
    InputLabel,
    OutlinedInput,
    InputAdornment,
    IconButton,
    Divider,
    Card,
    CardContent
} from '@mui/material';

import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';

import * as Yup from 'yup';
import { Formik } from 'formik';

import { useNavigate, Link as RouterLink } from 'react-router-dom';
import { login } from '../../services/authService';
import { useAuth } from '../../context/AuthContext';

import Logo from 'assets/images/logo-dark.svg';

const AuthLogin = () => {
    const theme = useTheme();
    const navigate = useNavigate();
    const { loginUser } = useAuth();

    const [showPassword, setShowPassword] = useState(false);

    const handleClickShowPassword = () => setShowPassword((show) => !show);
    const handleMouseDownPassword = (event) => event.preventDefault();

    return (
        <Grid
            container
            justifyContent="center"
            alignItems="center"
            sx={{
                backgroundColor: theme.palette.common.black,
                height: '100%',
                minHeight: '100vh',
            }}
        >
            <Grid item xs={11} sm={7} md={6} lg={4}>
                <Card
                    sx={{
                        overflow: 'visible',
                        display: 'flex',
                        position: 'relative',
                        '& .MuiCardContent-root': {
                            flexGrow: 1,
                            flexBasis: '50%',
                            width: '50%'
                        },
                        maxWidth: '475px',
                        margin: '24px auto'
                    }}
                >
                    <CardContent sx={{ p: theme.spacing(5, 4, 3, 4) }}>
                        <Grid container direction="column" spacing={4} justifyContent="center">
                            <Grid item xs={12}>
                                <Grid container alignItems="center" justifyContent="space-between">
                                    <Grid item>
                                        <Typography color="textPrimary" gutterBottom variant="h2">
                                            Sign In
                                        </Typography>
                                        <Typography variant="body2" color="textSecondary">
                                            Please enter your email and password
                                        </Typography>
                                    </Grid>
                                    <Grid item>
                                        <RouterLink to="/">
                                            <img alt="Auth method" src={Logo} style={{ height: 40 }} />
                                        </RouterLink>
                                    </Grid>
                                </Grid>
                            </Grid>

                            <Grid item xs={12}>
                                <Formik
                                    initialValues={{
                                        email: '',
                                        password: '',
                                        submit: null
                                    }}
                                    validationSchema={Yup.object().shape({
                                        email: Yup.string().email('Please enter a valid email address').required('Email is required'),
                                        password: Yup.string().required('Password is required')
                                    })}
                                    onSubmit={async (values, { setErrors, setSubmitting }) => {
                                        try {
                                            const response = await login(values.email, values.password);
                                            console.log("response-----:", response);
                                            loginUser({ accessToken: response.accessToken, refreshToken: response.refreshToken });
                                            navigate('/');
                                        } catch (error) {
                                            console.error(error);
                                            setErrors({ submit: 'Email or password is incorrect.' });
                                        } finally {
                                            setSubmitting(false);
                                        }
                                    }}
                                >
                                    {({ errors, handleBlur, handleChange, handleSubmit, isSubmitting, touched, values }) => (
                                        <form noValidate onSubmit={handleSubmit}>
                                            <Grid container direction="column" spacing={2}>
                                                <Grid item>
                                                    <TextField
                                                        fullWidth
                                                        label="Email Address"
                                                        name="email"
                                                        type="email"
                                                        value={values.email}
                                                        onChange={handleChange}
                                                        onBlur={handleBlur}
                                                        error={Boolean(touched.email && errors.email)}
                                                        helperText={touched.email && errors.email}
                                                    />
                                                </Grid>

                                                <Grid item>
                                                    <FormControl
                                                        fullWidth
                                                        variant="outlined"
                                                        error={Boolean(touched.password && errors.password)}
                                                    >
                                                        <InputLabel htmlFor="password">Password</InputLabel>
                                                        <OutlinedInput
                                                            id="password"
                                                            name="password"
                                                            type={showPassword ? 'text' : 'password'}
                                                            value={values.password}
                                                            onChange={handleChange}
                                                            onBlur={handleBlur}
                                                            label="Þifre"
                                                            endAdornment={
                                                                <InputAdornment position="end">
                                                                    <IconButton
                                                                        onClick={handleClickShowPassword}
                                                                        onMouseDown={handleMouseDownPassword}
                                                                        edge="end"
                                                                    >
                                                                        {showPassword ? <Visibility /> : <VisibilityOff />}
                                                                    </IconButton>
                                                                </InputAdornment>
                                                            }
                                                        />
                                                        {touched.password && errors.password && (
                                                            <FormHelperText error>{errors.password}</FormHelperText>
                                                        )}
                                                    </FormControl>
                                                </Grid>

                                                {errors.submit && (
                                                    <Grid item>
                                                        <FormHelperText error>{errors.submit}</FormHelperText>
                                                    </Grid>
                                                )}

                                                <Grid item>
                                                    <Button
                                                        fullWidth
                                                        variant="contained"
                                                        color="primary"
                                                        type="submit"
                                                        disabled={isSubmitting}
                                                    >
                                                        Login
                                                    </Button>
                                                </Grid>

                                                <Grid item>
                                                    <Divider sx={{ my: 2 }}>OR</Divider>
                                                </Grid>

                                                <Grid item>
                                                    <Button
                                                        fullWidth
                                                        variant="outlined"
                                                        color="secondary"
                                                        onClick={() => {
                                                            console.log('Sign in with Google');
                                                        }}
                                                    >
                                                        Sign in with Google
                                                    </Button>
                                                </Grid>
                                            </Grid>
                                        </form>
                                    )}
                                </Formik>
                            </Grid>
                        </Grid>
                    </CardContent>
                </Card>
            </Grid>
        </Grid>
    );
};

export default AuthLogin;
