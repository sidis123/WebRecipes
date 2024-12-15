import React, { useEffect, useState } from 'react'
import classNames from 'classnames'
import axios from 'axios'
import {
  CCard,
  CCardBody,
  CCardImage,
  CCardTitle,
  CRow,
  CCol,
  CModal,
  CFormSelect,
} from '@coreui/react'

import { CProgress } from '@coreui/react'
import CIcon from '@coreui/icons-react'

import { useSelector } from 'react-redux'
import RecipeCard from './RecipeCard'

const Dashboard = () => {
  const [recipes, setRecipes] = useState([])
  const user = useSelector((state) => state.user)
  const [categories, setCategories] = useState([])
  const [selectedCategory, setSelectedCategory] = useState('') // Selected category

  const [recipiesLoading, setRecipiesLoading] = useState(true)

  const token = localStorage.getItem('token')

  useEffect(() => {
    console.log('User:', user)
    fetchRecipes()
    fetchCategories()
    if (!recipiesLoading) {
      console.log('Recipes:', recipes)
    }
  }, [])

  const fetchCategories = async () => {
    axios
      .get('https://localhost:7120/api/Category', {
        headers: { Authorization: `Bearer ${token}` },
      })
      .then((response) => {
        setCategories(response.data) // Store categories
      })
      .catch((error) => {
        console.error('Error fetching categories:', error)
      })
  }
  const fetchRecipes = async (categoryId = '') => {
    setRecipiesLoading(true)
    const url = categoryId
      ? `https://localhost:7120/api/Category/${categoryId}/receptai` // Filtered by category
      : 'https://localhost:7120/api/Recipe' // All recipes

    axios
      .get(url, {
        headers: { Authorization: `Bearer ${token}` },
      })
      .then((response) => {
        setRecipes(categoryId ? response.data.receptai : response.data) // Handle filtered recipes
        setRecipiesLoading(false)
      })
      .catch((error) => {
        setRecipiesLoading(false)
        console.error('Error fetching recipes:', error)
      })
  }

  const handleCategoryChange = (e) => {
    const selectedValue = e.target.value
    setSelectedCategory(selectedValue) // Update selected category
    fetchRecipes(selectedValue) // Fetch filtered recipes
  }

  if (recipiesLoading) {
    return (
      <div className="d-flex justify-content-center align-items-center">
        <CProgress color="info" />
      </div>
    )
  } else {
    return (
      <div>
        <CRow className="mb-4 align-items-center">
          <CCol xs={12} md={4} lg={3}>
            <label style={{ fontSize: '25px' }}>Filter Recipes By Category</label>
            <CFormSelect
              id="Category"
              name="Category"
              onChange={handleCategoryChange}
              value={selectedCategory}
              options={[
                { label: 'All', value: '' },
                ...categories.map((cat) => ({
                  label: cat.pavadinimas,
                  value: cat.id_Kategorija,
                })),
              ]}
              style={{ maxWidth: '100%', minWidth: '200px' }}
            />
          </CCol>
        </CRow>
        {recipes.length > 0 ? (
          <CRow className="g-4">
            {recipes.map((recipe) => (
              <RecipeCard key={recipe.id} recipe={recipe} />
            ))}
          </CRow>
        ) : (
          <div className="empty">
            <h2>No Recipes Found</h2>
          </div>
        )}
      </div>
    )
  }
}

export default Dashboard
