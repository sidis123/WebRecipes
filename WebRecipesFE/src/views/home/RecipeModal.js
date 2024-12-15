import React from 'react'
import { CButton } from '@coreui/react'
import './RecipeModal.css' // Styles for text and image only
import { useNavigate } from 'react-router-dom'
import axios from 'axios'
import AllRecipeComments from './Comments/AllRecipeComments'
import CreateComment from './Comments/CreateComment'
import { useSelector } from 'react-redux'

const RecipeModal = ({ recipe }) => {
  const token = localStorage.getItem('token')
  const navigate = useNavigate() // React Router navigation hook
  const [needsRefresh, setNeedsRefresh] = React.useState(false)
  const user = useSelector((state) => state.user)
  const refreshComments = () => {
    setNeedsRefresh(true)
  }
  const refreshStatus = () => {
    setNeedsRefresh(false)
  }

  const EditOrderPage = () => {
    navigate(`/edit-recipe/${recipe.id}`) // Navigate without reload
  }

  const DeleteRecipe = () => {
    axios
      .delete(`https://localhost:7120/api/Recipe/${recipe.id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        alert('Recipe deleted successfully!')
        navigate('/')
      })
      .catch((error) => {
        console.error('Error deleting recipe:', error)
        alert('Failed to delete recipe.')
      })
  }
  return (
    <>
      <div
        style={{ padding: '20px', margin: '20px', border: '2px solid gray', borderRadius: '10px' }}
      >
        <div className="image-wrapper">
          <img src={recipe.pictureUrl} alt={recipe.pavadinimas} className="recipe-image" />
        </div>

        <h2 className="recipe-title">{recipe.pavadinimas}</h2>
        <h3 className="instructions-title">Aprašymas:</h3>
        <p className="recipe-description">{recipe.tekstas}</p>

        <h3 className="instructions-title">Gaminimo instrukcija:</h3>
        <p className="recipe-instructions">{recipe.instrukcija}</p>
        {(user?.role === 3 || (user?.role >= 2 && user.id_Vartotojas === recipe.kurejas)) && (
          <div className="button-container">
            <CButton color="primary" className="me-2" onClick={EditOrderPage}>
              Edit
            </CButton>
            <CButton color="danger" onClick={DeleteRecipe}>
              Delete
            </CButton>
          </div>
        )}
      </div>
      <CreateComment recipeId={recipe.id} onCommentCreated={refreshComments} />
      <AllRecipeComments
        recipeId={recipe.id}
        refetch={needsRefresh}
        setNeedsRefresh={refreshStatus}
      />
    </>
  )
}

export default RecipeModal
