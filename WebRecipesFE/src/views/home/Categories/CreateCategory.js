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
const CreateCategory = ({ categoryId, needRefetch }) => {
  const [state, setState] = useState({
    pavadinimas: '',
  })

  const token = localStorage.getItem('token')
  const userId = localStorage.getItem('userid')
  const [categories, setCategories] = useState([])

  useEffect(() => {
    if (!categoryId) {
      axios
        .get('https://localhost:7120/api/Category', {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((response) => {
          setCategories(response.data)
        })
    }
  }, [])

  const EditCategory = (e) => {
    e.preventDefault()
    axios
      .put(
        `https://localhost:7120/api/Category/${categoryId}`,
        {
          id_Kategorija: categoryId,
          pavadinimas: state.pavadinimas,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        },
      )
      .then((response) => {
        alert('category edited successfully!')
      })
      .catch((error) => {
        console.error('Error editing category:', error)
        alert('Failed to edit category.')
      })
  }

  const getCategoryById = (categoryId) => {
    axios
      .get(`https://localhost:7120/api/Category/${categoryId}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        setState({
          pavadinimas: response.data.pavadinimas,
        })
      })
      .catch((error) => {
        console.error('Error fetching category:', error)
      })
  }

  useEffect(() => {
    if (categoryId) {
      getCategoryById(categoryId)
    }
  }, [])

  const createCategory = (e) => {
    e.preventDefault()
    if (state.pavadinimas.trim() !== '') {
      axios
        .post(
          `https://localhost:7120/api/Category`,
          {
            pavadinimas: state.pavadinimas,
          },
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          },
        )
        .then((response) => {
          console.log('Category created successfully:', response.data)
          alert('Category created successfully!')
          setState({
            pavadinimas: '',
          })
          needRefetch()
        })
        .catch((error) => {
          console.error('Error creating Category:', error)
          alert('Failed to create Category.')
        })
    }
  }

  const handleChange = (e) => {
    setState({
      ...state,
      [e.target.name]: e.target.value,
    })
  }
  if (!categoryId) {
    return (
      <CRow className="justify-content-center">
        <CCol xs={12} md={12} lg={12}>
          <CCard>
            <CCardHeader>
              <h1 className="mb-0">Create Category</h1>
            </CCardHeader>
            <CCardBody>
              <form>
                <CRow className="mb-3">
                  <CCol xs={12} md={6} lg={6}>
                    <CFormInput
                      type="text"
                      id="pavadinimas"
                      label="Pavadinimas"
                      name="pavadinimas"
                      onChange={handleChange}
                      value={state.pavadinimas}
                    />
                  </CCol>
                </CRow>
                <div className="text-end">
                  <CButton
                    disabled={state.pavadinimas.trim() === '' ? true : false}
                    color="primary"
                    type="submit"
                    className="me-2"
                    onClick={createCategory}
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
  } else if (categoryId) {
    return (
      <CRow className="justify-content-center">
        <CCol xs={12} md={12} lg={12}>
          <CCard>
            <CCardHeader>
              <h1 className="mb-0">Edit Category</h1>
            </CCardHeader>
            <CCardBody>
              <form>
                <CRow className="mb-3">
                  <CCol xs={12} md={6} lg={6}>
                    <CFormInput
                      type="text"
                      id="pavadinimas"
                      label="Pavadinimas"
                      name="pavadinimas"
                      onChange={handleChange}
                      value={state.pavadinimas}
                    />
                  </CCol>
                </CRow>
                <div className="text-end">
                  <CButton
                    disabled={state.pavadinimas.trim() === '' ? true : false}
                    color="primary"
                    type="submit"
                    className="me-2"
                    onClick={EditCategory}
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

export default CreateCategory
