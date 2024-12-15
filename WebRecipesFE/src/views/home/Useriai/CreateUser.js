import React, { useEffect, useState } from 'react'
import {
  CButton,
  CCard,
  CCardBody,
  CCardHeader,
  CCol,
  CFormInput,
  CRow,
  CFormSelect,
} from '@coreui/react'
import axios from 'axios'
const CreateUser = ({ userioId, needRefetch }) => {
  const [state, setState] = useState({
    vardas: '',
    pavarde: '',
    email: '',
    password: '',
    telefonas: '',
    role: '',
  })

  const token = localStorage.getItem('token')
  const userId = localStorage.getItem('userid')
  const [categories, setCategories] = useState([])

  // useEffect(() => {
  //   if (!userioId) {
  //     axios
  //       .get('https://localhost:7120/api/Category', {
  //         headers: {
  //           Authorization: `Bearer ${token}`,
  //         },
  //       })
  //       .then((response) => {
  //         setCategories(response.data)
  //       })
  //   }
  // }, [])

  const EditUser = (e) => {
    e.preventDefault()
    axios
      .put(
        `https://localhost:7120/api/User/${userioId}`,
        {
          id_Vartotojas: userioId,
          vardas: state.vardas,
          pavarde: state.pavarde,
          email: state.email,
          password: state.password,
          telefonas: state.telefonas,
          role: state.role,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        },
      )
      .then((response) => {
        alert('User edited successfully!')
      })
      .catch((error) => {
        console.error('Error editing user:', error)
        alert('Failed to edit user.')
      })
  }

  const getUserById = (userioId) => {
    axios
      .get(`https://localhost:7120/api/User/${userioId}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        setState({
          vardas: response.data.vardas,
          pavarde: response.data.pavarde,
          email: response.data.email,
          password: response.data.password,
          telefonas: response.data.telefonas,
          role: response.data.role,
        })
      })
      .catch((error) => {
        console.error('Error fetching user:', error)
      })
  }

  useEffect(() => {
    if (userioId) {
      getUserById(userioId)
    }
  }, [])

  const createUser = (e) => {
    e.preventDefault()
    if (state.email.trim() !== '') {
      axios
        .post(
          `https://localhost:7120/api/User`,
          {
            vardas: state.vardas,
            pavarde: state.pavarde,
            email: state.email,
            password: state.password,
            telefonas: state.telefonas,
            role: state.role,
          },
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          },
        )
        .then((response) => {
          console.log('User created successfully:', response.data)
          alert('User created successfully!')
          setState({
            vardas: '',
            pavarde: '',
            email: '',
            password: '',
            telefonas: '',
            role: '',
          })
          needRefetch()
        })
        .catch((error) => {
          console.error('Error creating User:', error)
          alert('Failed to create User.')
        })
    }
  }

  const handleChange = (e) => {
    setState({
      ...state,
      [e.target.name]: e.target.value,
    })
  }
  if (!userioId) {
    return (
      <CRow className="justify-content-center">
        <CCol xs={12} md={12} lg={12}>
          <CCard>
            <CCardHeader>
              <h1 className="mb-0">Create User</h1>
            </CCardHeader>
            <CCardBody>
              <form>
                <CRow className="mb-3">
                  <CCol xs={12} md={6} lg={6}>
                    <CFormInput
                      type="text"
                      id="vardas"
                      label="Vardas"
                      name="vardas"
                      onChange={handleChange}
                      value={state.vardas}
                    />
                  </CCol>
                  <CCol xs={12} md={6} lg={6}>
                    <CFormInput
                      type="text"
                      id="pavarde"
                      label="Pavarde"
                      name="pavarde"
                      onChange={handleChange}
                      value={state.pavarde}
                    />
                  </CCol>
                </CRow>
                <CRow className="mb-3">
                  <CCol xs={12} md={6} lg={6}>
                    <CFormInput
                      type="text"
                      id="email"
                      label="Email"
                      name="email"
                      onChange={handleChange}
                      value={state.email}
                    />
                  </CCol>
                  <CCol xs={12} md={6} lg={6}>
                    <CFormInput
                      type="text"
                      id="password"
                      label="Password"
                      name="password"
                      onChange={handleChange}
                      value={state.password}
                    />
                  </CCol>
                </CRow>
                <CRow className="mb-3">
                  <CCol xs={12} md={6} lg={6}>
                    <CFormInput
                      type="text"
                      id="telefonas"
                      label="Telefonas"
                      name="telefonas"
                      onChange={handleChange}
                      value={state.telefonas}
                    />
                  </CCol>
                  <CCol xs={12} md={6} lg={6}>
                    <CFormInput
                      type="number"
                      id="role"
                      label="Role"
                      name="role"
                      onChange={handleChange}
                      value={state.role}
                    />
                  </CCol>
                </CRow>
                <div className="text-end">
                  <CButton
                    disabled={state.email.trim() === '' ? true : false}
                    color="primary"
                    type="submit"
                    className="me-2"
                    onClick={createUser}
                  >
                    Create
                  </CButton>
                </div>
              </form>
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>
    )
  } else if (userioId) {
    return (
      <CRow className="justify-content-center">
        <CCol xs={12} md={12} lg={12}>
          <CCard>
            <CCardHeader>
              <h1 className="mb-0">Edit User</h1>
            </CCardHeader>
            <CCardBody>
              <form>
                <CRow className="mb-3">
                  <CCol xs={12} md={6} lg={6}>
                    <CFormInput
                      type="text"
                      id="vardas"
                      label="Vardas"
                      name="vardas"
                      onChange={handleChange}
                      value={state.vardas}
                    />
                  </CCol>
                  <CCol xs={12} md={6} lg={6}>
                    <CFormInput
                      type="text"
                      id="pavarde"
                      label="Pavarde"
                      name="pavarde"
                      onChange={handleChange}
                      value={state.pavarde}
                    />
                  </CCol>
                </CRow>
                <CRow className="mb-3">
                  <CCol xs={12} md={6} lg={6}>
                    <CFormInput
                      type="text"
                      id="email"
                      label="Email"
                      name="email"
                      onChange={handleChange}
                      value={state.email}
                    />
                  </CCol>
                  <CCol xs={12} md={6} lg={6}>
                    <CFormInput
                      type="text"
                      id="password"
                      label="Password"
                      name="password"
                      onChange={handleChange}
                      value={state.password}
                    />
                  </CCol>
                </CRow>
                <CRow className="mb-3">
                  <CCol xs={12} md={6} lg={6}>
                    <CFormInput
                      type="text"
                      id="telefonas"
                      label="Telefonas"
                      name="telefonas"
                      onChange={handleChange}
                      value={state.telefonas}
                    />
                  </CCol>
                  <CCol xs={12} md={6} lg={6}>
                    <CFormInput
                      type="number"
                      id="role"
                      label="Role"
                      name="role"
                      onChange={handleChange}
                      value={state.role}
                    />
                  </CCol>
                </CRow>
                <div className="text-end">
                  <CButton
                    disabled={state.email.trim() === '' ? true : false}
                    color="primary"
                    type="submit"
                    className="me-2"
                    onClick={EditUser}
                  >
                    Edit
                  </CButton>
                </div>
              </form>
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>
    )
  }
}

export default CreateUser
