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
import { use } from 'react'

const CreateRecipe = ({ recipeId }) => {
  const [state, setState] = useState({
    pavadinimas: '',
    tekstas: '',
    instrukcija: '',
    Category: '',
    pictureUrl: '',
  })

  const token = localStorage.getItem('token')
  const userId = localStorage.getItem('userid')
  const [categories, setCategories] = useState([])

  useEffect(() => {
    if (!recipeId) {
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

  const EditRecipe = (e) => {
    e.preventDefault()
    axios
      .put(
        `https://localhost:7120/api/Recipe/${recipeId}`,
        {
          pavadinimas: state.pavadinimas,
          tekstas: state.tekstas,
          instrukcija: state.instrukcija,
          pictureUrl: state.pictureUrl,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        },
      )
      .then((response) => {
        alert('Recipe edited successfully!')
      })
      .catch((error) => {
        console.error('Error editing recipe:', error)
        alert('Failed to edit recipe.')
      })
  }

  const getRecipeById = (id) => {
    axios
      .get(`https://localhost:7120/api/Recipe/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        setState({
          pavadinimas: response.data.pavadinimas,
          tekstas: response.data.tekstas,
          instrukcija: response.data.instrukcija,
          Category: response.data.id_Kategorija,
          pictureUrl: response.data.pictureUrl,
        })
      })
      .catch((error) => {
        console.error('Error fetching recipe:', error)
      })
  }

  useEffect(() => {
    if (recipeId) {
      getRecipeById(recipeId)
    }
  }, [])

  const createRecipe = (e) => {
    e.preventDefault()

    if (!state.Category || !userId) {
      alert('Please select a category and ensure you are logged in.')
      return
    }

    axios
      .post(
        `https://localhost:7120/api/Recipe?categoryId=${state.Category}&userId=${userId}`,
        {
          pavadinimas: state.pavadinimas,
          tekstas: state.tekstas,
          instrukcija: state.instrukcija,
          pictureUrl: state.pictureUrl,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        },
      )
      .then((response) => {
        console.log('Recipe created successfully:', response.data)
        alert('Recipe created successfully!')
        setState({
          pavadinimas: '',
          tekstas: '',
          instrukcija: '',
          Category: '',
          pictureUrl: '',
        })
      })
      .catch((error) => {
        console.error('Error creating recipe:', error)
        alert('Failed to create recipe.')
      })
  }

  const handleChange = (e) => {
    setState({
      ...state,
      [e.target.name]: e.target.value,
    })
  }
  if (!recipeId) {
    return (
      <CRow className="justify-content-center">
        <CCol xs={12} md={12} lg={12}>
          <CCard>
            <CCardHeader>
              <h1 className="mb-0">Create Recipe</h1>
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
                  <CCol xs={12} md={6}>
                    <label htmlFor="Category" className="form-label">
                      Category
                    </label>
                    <CFormSelect
                      id="Category"
                      name="Category"
                      onChange={handleChange}
                      value={state.Category}
                      options={[
                        { label: 'Select a Category', value: '' },
                        ...categories.map((cat) => ({
                          label: cat.pavadinimas,
                          value: cat.id_Kategorija,
                        })),
                      ]}
                    />
                  </CCol>
                </CRow>
                <CRow className="mb-3">
                  <CCol xs={12} md={12} lg={12}>
                    <CFormInput
                      type="text"
                      id="tekstas"
                      label="Aprašymas"
                      name="tekstas"
                      onChange={handleChange}
                      value={state.tekstas}
                    />
                  </CCol>
                </CRow>
                <CRow className="mb-3">
                  <CCol xs={12} md={12} lg={12}>
                    <CFormInput
                      type="text"
                      id="instrukcija"
                      label="Instrukcija"
                      name="instrukcija"
                      onChange={handleChange}
                      value={state.instrukcija}
                    />
                  </CCol>
                </CRow>
                <CRow className="mb-3">
                  <CCol xs={12} md={12} lg={12}>
                    <CFormInput
                      type="text"
                      id="pictureUrl"
                      label="Nuotraukos URL"
                      name="pictureUrl"
                      onChange={handleChange}
                      value={state.pictureUrl}
                    />
                  </CCol>
                </CRow>
                <div className="text-end">
                  <CButton color="primary" type="submit" className="me-2" onClick={createRecipe}>
                    Create
                  </CButton>
                </div>
              </form>
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>
    )
  } else if (recipeId) {
    return (
      <CRow className="justify-content-center">
        <CCol xs={12} md={12} lg={12}>
          <CCard>
            <CCardHeader>
              <h1 className="mb-0">Edit Recipe</h1>
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
                <CRow className="mb-3">
                  <CCol xs={12} md={12} lg={12}>
                    <CFormInput
                      type="text"
                      id="tekstas"
                      label="Aprašymas"
                      name="tekstas"
                      onChange={handleChange}
                      value={state.tekstas}
                    />
                  </CCol>
                </CRow>
                <CRow className="mb-3">
                  <CCol xs={12} md={12} lg={12}>
                    <CFormInput
                      type="text"
                      id="instrukcija"
                      label="Instrukcija"
                      name="instrukcija"
                      onChange={handleChange}
                      value={state.instrukcija}
                    />
                  </CCol>
                </CRow>
                <CRow className="mb-3">
                  <CCol xs={12} md={12} lg={12}>
                    <CFormInput
                      type="text"
                      id="pictureUrl"
                      label="Nuotraukos URL"
                      name="pictureUrl"
                      onChange={handleChange}
                      value={state.pictureUrl}
                    />
                  </CCol>
                </CRow>
                <div className="text-end">
                  <CButton color="primary" type="button" className="me-2" onClick={EditRecipe}>
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

export default CreateRecipe
