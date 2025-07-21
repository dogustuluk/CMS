import React from 'react';
import { useNavigate, Link as RouterLink } from 'react-router-dom';

// material-ui
import { useTheme } from '@mui/material/styles';
import { Card, CardContent, Typography, Grid } from '@mui/material';

// project import
import AuthRegister from './AuthRegister';

// assets
import Logo from 'assets/images/logo-dark.svg';

// --
import { Formik } from 'formik';
import * as Yup from 'yup';
import { useAuth } from '../../context/AuthContext';

// ==============================|| REGISTER ||============================== //

const Register = () => {
    const theme = useTheme();
    const navigate = useNavigate();
    const { registerUser } = useAuth();

    return (
        <Formik
            initialValues=
            {{
                username: '',
                email: '',
                password: '',
                role: 0,
                tenantName: '',
                domainName: '',
            }}
            validationSchema={Yup.object().shape({
                username: Yup.string().required('Username is required'),
                email: Yup.string().email('Invalid email').required('Email is required'),
                password: Yup.string().min(6).required('Password is required'),
                tenantName: Yup.string().required('Tenant name is required'),
                domainName: Yup.string().required('Domain name is required')
            })}
            onSubmit={async (values, { setSubmitting, setErrors }) => {
                try {
                    await registerUser(values.username, values.email, values.password, values.role, values.tenantName, values.domainName);
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
            <Grid
                container
                justifyContent="center"
                alignItems="center"
                sx={{ backgroundColor: theme.palette.common.black, height: '100%', minHeight: '100vh' }}
            >
                <Grid item xs={11} md={6} lg={4}>
                    <Card
                        sx={{
                            overflow: 'visible',
                            display: 'flex',
                            position: 'relative',
                            my: 3,
                            mx: 'auto',
                            '& .MuiCardContent-root': {
                                flexGrow: 1,
                                flexBasis: '50%',
                                width: '50%'
                            },
                            maxWidth: 475
                        }}
                    >
                        <CardContent sx={{ p: theme.spacing(5, 4, 3, 4) }}>
                            <Grid container direction="column" spacing={4} justifyContent="center">
                                <Grid item xs={12}>
                                    <Grid container justifyContent="space-between">
                                        <Grid item>
                                            <Typography color="textPrimary" gutterBottom variant="h2">
                                                Register
                                            </Typography>
                                            <Typography variant="body2" color="textSecondary">
                                                To keep connected with us.
                                            </Typography>
                                        </Grid>
                                        <Grid item>
                                            <RouterLink to="/">
                                                <img alt="Auth method" src={Logo} />
                                            </RouterLink>
                                        </Grid>
                                    </Grid>
                                </Grid>
                                <Grid item xs={12}>
                                    <AuthRegister />
                                </Grid>
                                <Grid container justifyContent="flex-start" sx={{ mt: theme.spacing(2), mb: theme.spacing(1) }}>
                                    <Grid item>
                                        <Typography
                                            variant="subtitle2"
                                            color="secondary"
                                            component={RouterLink}
                                            to="/application/login"
                                            sx={{ textDecoration: 'none', pl: 2 }}
                                        >
                                            Having an account
                                        </Typography>
                                    </Grid>
                                </Grid>
                            </Grid>
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>
        </Formik>
    );
};

export default Register;
