import React, { useEffect, useState } from 'react'
import classNames from 'classnames'
import axios from 'axios'
import { CCard, CCardBody, CCardImage, CCardTitle, CRow, CCol, CModal } from '@coreui/react'

import { CProgress } from '@coreui/react'
import CIcon from '@coreui/icons-react'

import { useSelector } from 'react-redux'
import RecipeCard from './RecipeCard'

const Dashboard = () => {
  const [recipes, setRecipes] = useState([])
  const user = useSelector((state) => state.user)
  const [recipiesLoading, setRecipiesLoading] = useState(true)

  useEffect(() => {
    console.log('User:', user)
    fetchRecipes()
    if (!recipiesLoading) {
      console.log('Recipes:', recipes)
    }
  }, [])

  const fetchRecipes = async () => {
    setRecipiesLoading(true)
    axios
      .get('https://localhost:7120/api/Recipe')
      .then((response) => {
        setRecipes(response.data)
        console.log('Recipes:', response.data)
        setRecipiesLoading(false)
      })
      .catch((error) => {
        setRecipiesLoading(false)
        console.error('Error fetching recipes:', error)
      })
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
