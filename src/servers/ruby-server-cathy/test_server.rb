require 'minitest/autorun'
require 'rack/test'
require_relative 'server'

class UserAPITestCase < Minitest::Test
  include Rack::Test::Methods

  def app
    Sinatra::Application
  end

  def setup
    # Reset the in-memory storage before each test
    @users = []
    @next_id = 1
  end

  def test_get_all_users
    get '/users'
    assert last_response.ok?
    assert_equal [], JSON.parse(last_response.body)
  end

  def test_add_user
    post '/users', { name: 'John Doe' }.to_json, { 'CONTENT_TYPE' => 'application/json' }
    assert_equal 201, last_response.status
    data = JSON.parse(last_response.body)
    assert_equal 1, data['id']
    assert_equal 'John Doe', data['name']
    assert_equal 0, data['hoursWorked']
  end

  def test_get_user_by_id
    post '/users', { name: 'John Doe' }.to_json, { 'CONTENT_TYPE' => 'application/json' }
    get '/users/1'
    assert last_response.ok?
    data = JSON.parse(last_response.body)
    assert_equal 1, data['id']
    assert_equal 'John Doe', data['name']
    assert_equal 0, data['hoursWorked']
  end

  def test_get_non_existent_user
    get '/users/999'
    assert_equal 404, last_response.status
    assert_equal 'User not found', last_response.body
  end

  def test_update_user
    post '/users', { name: 'John Doe' }.to_json, { 'CONTENT_TYPE' => 'application/json' }
    put '/users/1', { name: 'Jane Doe' }.to_json, { 'CONTENT_TYPE' => 'application/json' }
    assert last_response.ok?
    data = JSON.parse(last_response.body)
    assert_equal 1, data['id']
    assert_equal 'Jane Doe', data['name']
  end

  def test_update_user_hours
    post '/users', { name: 'John Doe' }.to_json, { 'CONTENT_TYPE' => 'application/json' }
    patch '/users/1', { hoursToAdd: 5 }.to_json, { 'CONTENT_TYPE' => 'application/json' }
    assert last_response.ok?
    data = JSON.parse(last_response.body)
    assert_equal 1, data['id']
    assert_equal 5, data['hoursWorked']
  end

  def test_delete_user
    post '/users', { name: 'John Doe' }.to_json, { 'CONTENT_TYPE' => 'application/json' }
    delete '/users/1'
    assert last_response.ok?
    data = JSON.parse(last_response.body)
    assert_equal 1, data['id']
  end

  def test_delete_all_users
    post '/users', { name: 'John Doe' }.to_json, { 'CONTENT_TYPE' => 'application/json' }
    delete '/users'
    assert last_response.ok?
    assert_equal [], JSON.parse(last_response.body)
  end
end
