# server.rb
require 'sinatra'
require 'json'
require 'sinatra/cors'

set :port, 5003

# Enable CORS
set :allow_origin, '*'
set :allow_methods, 'GET,POST,PUT,PATCH,DELETE,OPTIONS'
set :allow_headers, 'content-type,if-modified-since'

# In-memory storage for users
users = []
next_id = 1

get '/users' do
  content_type :json
  users.to_json
end

get '/users/:id' do
  content_type :json
  user = users.find { |u| u[:id] == params[:id].to_i }
  if user
    user.to_json
  else
    status 404
    'User not found'
  end
end

delete '/users' do
  users.clear
  next_id = 1
  content_type :json
  users.to_json
end

post '/users' do
  content_type :json
  request_body = JSON.parse(request.body.read)
  name = request_body['name']
  if name.nil? || !name.is_a?(String) || name.strip.empty?
    status 400
    'Name is required and must be a non-empty string'
  else
    new_user = { id: next_id, name: name.strip, hoursWorked: 0 }
    users << new_user
    next_id += 1
    status 201
    new_user.to_json
  end
end

put '/users/:id' do
  content_type :json
  user = users.find { |u| u[:id] == params[:id].to_i }
  if user
    request_body = JSON.parse(request.body.read)
    name = request_body['name']
    user[:name] = name.strip if name && name.is_a?(String) && !name.strip.empty?
    user.to_json
  else
    status 404
    'User not found'
  end
end

patch '/users/:id' do
  content_type :json
  user = users.find { |u| u[:id] == params[:id].to_i }
  if user
    request_body = JSON.parse(request.body.read)
    hours_to_add = request_body['hoursToAdd']
    if hours_to_add.is_a?(Numeric)
      user[:hoursWorked] += hours_to_add
      user.to_json
    else
      status 400
      'Invalid hoursToAdd value'
    end
  else
    status 404
    'User not found'
  end
end

delete '/users/:id' do
  user_index = users.index { |u| u[:id] == params[:id].to_i }
  if user_index
    deleted_user = users.delete_at(user_index)
    content_type :json
    deleted_user.to_json
  else
    status 404
    'User not found'
  end
end
