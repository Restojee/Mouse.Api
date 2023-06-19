import { JwtAuthGuard } from "../auth/guards/jwt-auth.guard";
import {
  Body,
  Controller,
  HttpStatus,
  Param,
  Post,
  UseGuards,
  Request, Get, Put
} from "@nestjs/common";
import {
  ApiBearerAuth,
  ApiOperation,
  ApiResponse,
  ApiTags
} from "@nestjs/swagger";
import { CommentUpdateRequest } from "./models/comment-update-request";
import { CommentCreateRequest } from "./models/comment-create-request";
import { CommentService } from "./comment.service";
import { Comment } from "./models/comment";

@ApiTags('Comments')
@Controller('comments')
@ApiBearerAuth()
export class CommentController {
  constructor(private commentService: CommentService) {}

  @Post("create")
  @UseGuards(JwtAuthGuard)
  @ApiResponse({ status: HttpStatus.OK, type: Comment })
  @ApiOperation({ operationId: 'CreateComment' })
  async createCommentByUser(
      @Request() request,
      @Body() commentCreateRequest: CommentCreateRequest,
  ) {
    const { mapId, text } = commentCreateRequest;
    const { id: userId } = request.user;
    return await this.commentService.createComment({ mapId, userId, text });
  }

  @Put("update")
  @UseGuards(JwtAuthGuard)
  @ApiResponse({ status: HttpStatus.OK, type: Comment })
  @ApiOperation({ operationId: 'UpdateComment' })
  async updateCommentByUser(
      @Request() request,
      @Body() commentUpdateRequest: CommentUpdateRequest,
  ) {
    const { commentId, text } = commentUpdateRequest;
    const { id: userId } = request.user;
    return await this.commentService.updateComment({ commentId, userId, text });
  }

  @Get("collect/by-map/:id")
  @UseGuards(JwtAuthGuard)
  @ApiResponse({ status: HttpStatus.OK, type: Comment })
  @ApiOperation({ operationId: 'GetCommentsByMap' })
  async getCommentsByMap(
      @Request() request,
      @Param('id') mapId: number
  ) {
    return await this.commentService.getCommentsByMap({ mapId  });
  }

  @Get("collect/by-user/:id")
  @UseGuards(JwtAuthGuard)
  @ApiResponse({ status: HttpStatus.OK, type: Comment })
  @ApiOperation({ operationId: 'GetCommentsByUser' })
  async getCommentsByUser(
      @Request() request,
      @Param('id') userId: number
  ) {
    return await this.commentService.getCommentsByUser({ userId });
  }
}
