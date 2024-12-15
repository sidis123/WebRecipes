import React, { useEffect, useState } from 'react'
import axios from 'axios'
import { CCard, CCardBody, CCardHeader, CRow, CCol, CButton, CFormInput } from '@coreui/react'
import { useSelector } from 'react-redux'

const AllRecipeComments = ({ recipeId, refetch, setNeedsRefresh }) => {
  const token = localStorage.getItem('token')
  const userId = localStorage.getItem('userid')
  const [comments, setComments] = useState([]) // State to hold comments
  const [editableCommentId, setEditableCommentId] = useState(null) // Tracks the comment being edited
  const [editedText, setEditedText] = useState('') // Tracks the text being edited
  const user = useSelector((state) => state.user)

  useEffect(() => {
    fetchComments()
  }, [])

  useEffect(() => {
    if (refetch) {
      fetchComments()
    }
  }, [refetch])

  const fetchComments = async () => {
    axios
      .get(`https://localhost:7120/api/Comment`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        const filteredComments = response.data.filter(
          (comment) => comment.recipeid_Receptas === recipeId,
        )
        setComments(filteredComments)
        setNeedsRefresh()
      })
      .catch((error) => {
        console.error('Error fetching comments:', error)
      })
  }

  const handleEditClick = (commentId, commentText, comment) => {
    // Toggle between editing mode and saving mode
    if (editableCommentId === commentId) {
      handleSaveEdit(commentId, comment) // Save if already in edit mode
    } else {
      setEditableCommentId(commentId) // Set edit mode
      setEditedText(commentText) // Set the current text to be edited
    }
  }

  const handleSaveEdit = (commentId, comment) => {
    axios
      .put(
        `https://localhost:7120/api/Comment/${commentId}`,
        {
          id_Komentaras: commentId,
          patiktukai: comment.patiktukai,
          sukurimo_data: new Date().toISOString(),
          tekstas: editedText,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        },
      )
      .then(() => {
        alert('Comment edited successfully!')
        setEditableCommentId(null) // Exit edit mode
        fetchComments() // Refresh comments
      })
      .catch((error) => {
        console.error('Error editing comment:', error)
        alert('Failed to edit comment.')
      })
  }

  const handleDelete = (commentId) => {
    axios
      .delete(`https://localhost:7120/api/Comment/${commentId}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then(() => {
        alert('Comment deleted successfully!')
        fetchComments() // Refresh comments
      })
      .catch((error) => {
        console.error('Error deleting comment:', error)
        alert('Failed to delete comment.')
      })
  }

  return (
    <CRow className="justify-content-center mt-4">
      <CCol xs={12} md={10}>
        <h2 className="mb-4 text-center">Recipe Comments</h2>
        {comments.length > 0 ? (
          comments.map((comment) => (
            <CCard key={comment.id_Komentaras} className="mb-3 shadow-sm">
              <CCardHeader>
                <strong>Comment ID:</strong> {comment.id_Komentaras} |{' '}
                <small>{new Date(comment.sukurimo_data).toLocaleString()}</small>
              </CCardHeader>
              <CCardBody>
                {/* Render editable input field or text */}
                {editableCommentId === comment.id_Komentaras ? (
                  <CFormInput
                    value={editedText}
                    onChange={(e) => setEditedText(e.target.value)}
                    className="mb-2"
                  />
                ) : (
                  <p>
                    <strong>Comment:</strong> {comment.tekstas}
                  </p>
                )}
                <div className="d-flex justify-content-between align-items-center">
                  {parseInt(comment.userid_Vartotojas) === parseInt(userId) && user?.role >= 2 ? (
                    <div className="d-flex ms-auto">
                      <CButton
                        color="info"
                        size="sm"
                        className="me-2"
                        onClick={() =>
                          handleEditClick(comment.id_Komentaras, comment.tekstas, comment)
                        }
                      >
                        {editableCommentId === comment.id_Komentaras ? 'Save' : 'Edit'}
                      </CButton>
                      <CButton
                        color="danger"
                        size="sm"
                        onClick={() => handleDelete(comment.id_Komentaras)}
                      >
                        Delete
                      </CButton>
                    </div>
                  ) : (
                    <span className="text-muted">User Comment</span>
                  )}
                </div>
              </CCardBody>
            </CCard>
          ))
        ) : (
          <p className="text-center text-muted">No comments available for this recipe.</p>
        )}
      </CCol>
    </CRow>
  )
}

export default AllRecipeComments
