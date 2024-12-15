import React, { useState } from 'react'
import { Link } from 'react-router-dom'
import axios from 'axios'
import { useDispatch } from 'react-redux'
import { useNavigate } from 'react-router-dom'
import {
  CButton,
  CCard,
  CCardBody,
  CCardGroup,
  CCol,
  CContainer,
  CForm,
  CFormInput,
  CInputGroup,
  CInputGroupText,
  CRow,
} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { cilLockLocked, cilUser } from '@coreui/icons'

const initialState = {
  username: '',
  password: '',
}

const Login = () => {
  const [state, setState] = useState(initialState)
  const dispatch = useDispatch()
  const navigate = useNavigate()
  const [loginError, setLoginError] = useState(false)
  const [isHovered, setIsHovered] = useState(false)

  const buttonStyle = {
    backgroundColor: isHovered ? '#0056b3' : '#007bff',
    color: isHovered ? '#f8f9fa' : 'white',
    transform: isHovered ? 'scale(1.05)' : 'scale(1)',
    boxShadow: isHovered ? '0 4px 8px rgba(0, 0, 0, 0.2)' : 'none',
    transition: 'all 0.3s ease',
    cursor: 'pointer',
    border: 'none',
    padding: '10px 20px',
    borderRadius: '4px',
  }

  const handleChange = (e) => {
    const { name, value } = e.target
    setState((prevState) => ({
      ...prevState,
      [name]: value,
    }))
    setLoginError(false)
  }

  const handleLogin = () => {
    axios
      .post('https://localhost:7120/api/User/login', {
        email: state.username,
        password: state.password,
      })
      .then((response) => {
        console.log('Logged in:', response.data.user)
        localStorage.setItem('token', response.data.accessToken)
        localStorage.setItem('userid', response.data.user.id_Vartotojas)
        dispatch({ type: 'set_user', user: response.data.user })
        navigate('/')
      })
      .catch((error) => {
        console.error('Error logging in:', error)
        setLoginError(true)
      })
  }

  return (
    <div className="bg-body-tertiary min-vh-100 d-flex flex-row align-items-center">
      <CContainer>
        <CRow className="justify-content-center">
          <CCol md={8}>
            <CCardGroup>
              <CCard className="p-4">
                <CCardBody>
                  <CForm>
                    <h1>Login</h1>
                    <p className="text-body-secondary">Sign In to your account</p>
                    {loginError && (
                      <p style={{ color: 'red', marginBottom: '10px' }}>
                        Username or password is incorrect.
                      </p>
                    )}
                    <CInputGroup className="mb-3">
                      <CInputGroupText>
                        <CIcon icon={cilUser} />
                      </CInputGroupText>
                      <CFormInput
                        name="username"
                        onChange={handleChange}
                        id="username"
                        placeholder="username or email"
                        type="text"
                        value={state.username}
                        style={{
                          borderColor: loginError ? 'red' : '',
                        }}
                      />
                    </CInputGroup>
                    <CInputGroup className="mb-4">
                      <CInputGroupText>
                        <CIcon icon={cilLockLocked} />
                      </CInputGroupText>
                      <CFormInput
                        name="password"
                        type="password"
                        id="password"
                        onChange={handleChange}
                        placeholder="password"
                        value={state.password}
                        style={{
                          borderColor: loginError ? 'red' : '',
                        }}
                      />
                    </CInputGroup>
                    <CRow>
                      <CCol xs={6}>
                        <CButton onClick={handleLogin} color="primary" className="px-4">
                          Login
                        </CButton>
                      </CCol>
                    </CRow>
                  </CForm>
                </CCardBody>
              </CCard>
              <CCard className="text-white bg-primary py-5" style={{ width: '44%' }}>
                <CCardBody className="text-center">
                  <div>
                    <h2>Sign up</h2>
                    <p>
                      You dont have an account ? Just Register and start using the greatest recipe
                      website ever !
                    </p>
                    <Link to="/register">
                      <CButton color="primary" className="mt-3" active tabIndex={-1}>
                        Register Now!
                      </CButton>
                    </Link>
                  </div>
                </CCardBody>
              </CCard>
            </CCardGroup>
            <CButton
              onClick={() => navigate('/')}
              style={buttonStyle}
              onMouseEnter={() => setIsHovered(true)} // Trigger hover state
              onMouseLeave={() => setIsHovered(false)} // Remove hover state
            >
              <CIcon className="me-2" />
              Continue using as guest
            </CButton>
          </CCol>
        </CRow>
      </CContainer>
    </div>
  )
}

export default Login
